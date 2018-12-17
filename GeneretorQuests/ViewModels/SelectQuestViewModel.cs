using BLL;
using DAL;
using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;
using GeneretorQuests.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
   public class SelectQuestViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDialogManager _dialogManager;
        public DbDataOperation rep;
        DBRepos d;
        private QuestModel selectedQuest;
        private RiddleModel selectedRiddle;
        private UserModel User;
        public QuestModel SelectedQuest
        {
            get { return selectedQuest; }
            set
            {
                selectedQuest = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedQuest"));
            }

        }
        public RiddleModel SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
            }

        }

        private ICommand openSelectRiddle;
        public ICommand OpenSelectRiddle
        {
            get
            {
                return openSelectRiddle ??
                      (openSelectRiddle = new RelayCommand(obj => new OpenSelectRiddleCommand(d,_dialogManager, SelectedRiddle, User, selectedQuest.Id_quest).Execute(obj),
                      (obj) => SelectedRiddle != null));
               
            }
        }
        private ICommand addRiddle;
        public ICommand AddRiddle
        {
            get
            {
                return addRiddle ??
                      (addRiddle = new RelayCommand(obj => new OpenSelectRiddleCommand(d,_dialogManager, SelectedRiddle, User, selectedQuest.Id_quest).Execute(obj)));

            }
        }
        private ICommand saveChanges;
        public ICommand SaveChanges
        {
            get
            {
                return saveChanges ??
                      (saveChanges = new RelayCommand(obj =>rep.UpdateQuest(toQuest(SelectedQuest))));

            }
        }
        public SelectQuestViewModel(IDialogManager dm, QuestModel q, UserModel user)
        {
            _dialogManager = dm;
            d = new DBRepos();
            rep = new DbDataOperation(d);
            selectedQuest = q;
            User = user;
            
        }

        
        private ObservableCollection<RiddleModel> GetListForQuest(int Id_quest)
        {
            // ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetList().SelectMany(u=>u.Quest, (u, q)=>new { Riddle=u, Quest=q}).Where(u=>u.Quest.Id_quest== Id_quest).Select(i => toRiddleModel(i)).ToList());
            ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.GetListRiddleForQuest(Id_quest).Select(i => toRiddleModel(i)).ToList());
            return riddls;
        }
        private QuestModel toQuestModel(Quest i)
        {
            return new QuestModel()
            {
                Id_quest = i.Id_quest,
                Status = i.Status,
                Number_of_questions = i.Number_of_questions,
                Thematics = i.Thematics,
                Id_Level_FK = i.Id_Level_FK,
                Id_Autor_FK = i.Id_Autor_FK,
                Autor_name = i.User.Name,
                Date = i.Date,
                Level_name = i.Level_of_complexity.Name_level,
                Riddle = GetListForQuest(i.Id_quest)
            };
        }
        private Quest toQuest(QuestModel i)
        {
            return new Quest()
            {
                Id_quest = i.Id_quest,
                Status = i.Status,
                Number_of_questions = i.Number_of_questions,
                Thematics = i.Thematics,
                Id_Level_FK = i.Id_Level_FK,
                Id_Autor_FK = i.Id_Autor_FK,
                User =rep.GetUser(i.Id_Autor_FK),
                Date = i.Date,
                Level_of_complexity=rep.GetLevel(i.Id_Level_FK),
                Riddle = new ObservableCollection<Riddle>(i.Riddle.Select(r => toRiddle(r)).ToList())
            };
        }
        private Riddle toRiddle(RiddleModel i)
        {
           
            return new Riddle()
            {
                Id_riddle = i.Id_riddle,
                Text = i.Text,
                Description = i.Description,
                Status = i.Status,
                Id_Autor_FK = User.Id_user,//selected?
                User = rep.GetUser(User.Id_user),
                Id_Level_FK = i.Id_Level_FK,
                Level_of_complexity = rep.GetLevel(i.Id_Level_FK),
                Id_Answer_FK = i.Id_Answer_FK,
                Answer = rep.GetAnswer(i.Id_Answer_FK),
                Id_Type_FK = i.Id_Type_FK,
                Type_of_question = rep.GetType(i.Id_Type_FK),
                Quest = i.Quest
                //Image
            };
        }
        private RiddleModel toRiddleModel(Riddle i)
        {
            return new RiddleModel()
            {
                Id_riddle = i.Id_riddle,
                Text = i.Text,
                Description = i.Description,
                Status = i.Status,
                Id_Autor_FK = i.Id_Autor_FK,
                Autor_name = i.User.Name,
                Id_Level_FK = i.Id_Level_FK,
                Level_name = i.Level_of_complexity.Name_level,
                Id_Answer_FK = i.Id_Answer_FK,
                Answer_name = i.Answer.Object,
                Id_Type_FK = i.Id_Type_FK,
                Type_name = i.Type_of_question.Name,
                Quest = i.Quest
                //Image
            };
        }
       
    }
}

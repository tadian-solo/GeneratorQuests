﻿using DAL; 
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;
using GeneretorQuests.ViewModels.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
    public class QuestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DBRepos rep;
        private QuestModel selectedQuest;
        private UserModel User;
       // private RiddleModel selectedRiddle;
          
        private readonly IDialogManager _dialogManager;
        public QuestViewModel(IDialogManager dialogManager, UserModel user)
        {
            _dialogManager = dialogManager;
            this.Quests = new ObservableCollection<QuestModel>();
            rep = new DBRepos();
            User = user;
            Quests = GetAllQuests();
        }
        /*private ICommand openSelectRiddle;
        public ICommand OpenSelectRiddle
        {
            get
            {
                if (openSelectRiddle == null) openSelectRiddle = new OpenSelectRiddleCommand(_dialogManager, SelectedRiddle);
                return openSelectRiddle;
            }
        }*/

        private ObservableCollection<QuestModel> _quests = null;
        public ObservableCollection<QuestModel> Quests
        {
            get { return _quests; }
            set
            {
                if (_quests != value)
                {
                    _quests = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Quests"));
                    }
                }
            }
        }
        public QuestModel SelectedQuest
        {
            get { return selectedQuest; }
            set
            {
                selectedQuest = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedQuest"));
                
            
            }

        }
        private RelayCommand openSelectQuest;
        public RelayCommand OpenSelectQuest
        {
            get
            {
                return openSelectQuest ??
                      (openSelectQuest = new RelayCommand(obj =>new OpenSelectQuestCommand(_dialogManager, SelectedQuest, User).Execute(obj),
                      (obj) => SelectedQuest != null));
            }
            
        }
        /*private OpenSelectQuestCommand openSelectQuest;
        public OpenSelectQuestCommand OpenSelectQuest
        {
            get
            {
                if (openSelectQuest==null) openSelectQuest = new OpenSelectQuestCommand(_dialogManager, SelectedQuest);
                if (openSelectQuest.qm != SelectedQuest) openSelectQuest.qm= SelectedQuest;
                    return openSelectQuest;
            }
            set
            {
                if (openSelectQuest.qm != SelectedQuest) openSelectQuest.qm = SelectedQuest;
            }
        }*/
        /*public RiddleModel SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
            }

        }*/
        /*public QuestViewModel()
        {


            this.Quests = new ObservableCollection<QuestModel>();
            rep = new DBRepos();
            Quests = GetAllQuests();
            /*ObservableCollection<Quest> quests =new ObservableCollection<Quest>();
            quests= rep.Quests.GetList();
            Records = ToQuestModel(quests);

        }*/

        private ObservableCollection<QuestModel> GetAllQuests()
        {
            ObservableCollection<QuestModel> quests = new ObservableCollection<QuestModel>(rep.Quests.GetList().Select(i => toQuestModel(i)).ToList());
            return quests;
        }
        private ObservableCollection<RiddleModel> GetListForQuest(int Id_quest)
        {
            // ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetList().SelectMany(u=>u.Quest, (u, q)=>new { Riddle=u, Quest=q}).Where(u=>u.Quest.Id_quest== Id_quest).Select(i => toRiddleModel(i)).ToList());
            ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetListForQuest(Id_quest).Select(i => toRiddleModel(i)).ToList());
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
                //Image
            };
        }
        
    }
}

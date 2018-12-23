using BLL;
using DAL;
using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;

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
   public class SelectQuestViewModel : BaseViewModel, INotifyPropertyChanged
    {
       // public Action CloseAction { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDialogManager _dialogManager;
        public DbDataOperation rep;
        private Quest selectedQuest;
        private Riddle selectedRiddle;
        private User User;
        public Quest SelectedQuest
        {
            get { return selectedQuest; }
            set
            {
                selectedQuest = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedQuest"));
            }

        }
        public Riddle SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
            }

        }
        private bool added;
        public bool Added
        {
            get { return added; }
            set
            {
                added = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("Added"));
            }
        }
        private ICommand openSelectRiddle;
        public ICommand OpenSelectRiddle//delete realy hehe
        {
            get
            {
                return openSelectRiddle ??
                      (openSelectRiddle = new RelayCommand(obj => DeleteRiddleForQuest(),
                      (obj) => SelectedRiddle != null));
               
            }
        }
        void DeleteRiddleForQuest()
        {
            var r = rep.GetRiddle(selectedRiddle.Id_riddle);
            r.Quest.Remove(SelectedQuest);
            rep.UpdateRiddle(r);
            SelectedQuest.Riddle.Remove(SelectedRiddle);
            rep.UpdateQuest(SelectedQuest);
            
        }
        private ICommand addRiddle;
        public ICommand AddRiddle
        {
            get
            {
                return addRiddle ??
                      (addRiddle = new RelayCommand(obj =>
                      {
                         
                          new OpenSelectRiddleCommand(d, _dialogManager, null, User, selectedQuest.Id_quest, false, false).Execute(obj);
                          //CloseAction();

                      }));

            }
        }

        private ICommand saveQuest;//
        public ICommand SaveQuest
        {
            get
            {
                return saveQuest ??
                      (saveQuest = new RelayCommand(obj => Save()));

            }
        }
        private void Save()
        {
             rep.UpdateQuest(SelectedQuest); 
        }
        public DBRepos d;
        public SelectQuestViewModel(IDialogManager dm, int q, User user, DBRepos db)
        {
            _dialogManager = dm;
            d = db;
            User = user;
            rep = new DbDataOperation(d);
            if (q != -1) selectedQuest = rep.GetQuest(q);
            else
            {
                selectedQuest = new Quest
                {
                    Status = User.Access_level,
                    Number_of_questions = 0,
                    Thematics = "no",
                    Id_Level_FK = 1,
                    Id_Autor_FK = User.Id_user,
                    //User = User,
                    Date = DateTime.Now,
                    //Level_of_complexity = rep.GetLevel(1)
                };
                rep.CreateQuest(selectedQuest);
            }
            selectedQuest.Updated += RefreshTable;
            
            
        }

        void RefreshTable()
        {
            SelectedQuest.Riddle = rep.GetListRiddleForQuest(selectedQuest.Id_quest);
        }
        private ObservableCollection<Riddle> GetListForQuest(int Id_quest)
        {
            // ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetList().SelectMany(u=>u.Quest, (u, q)=>new { Riddle=u, Quest=q}).Where(u=>u.Quest.Id_quest== Id_quest).Select(i => toRiddleModel(i)).ToList());
            ObservableCollection<Riddle> riddls = new ObservableCollection<Riddle>(rep.GetListRiddleForQuest(Id_quest));
            return riddls;
        }
        
       
    }
}

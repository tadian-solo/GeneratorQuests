using BLL;
using DAL; 
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
    public class QuestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DbDataOperation rep;
        public DBRepos db;
        private Quest selectedQuest;
        private User User;
       // private RiddleModel selectedRiddle;
          
        private readonly IDialogManager _dialogManager;
        public QuestViewModel(IDialogManager dialogManager, User user)
        {
            _dialogManager = dialogManager;
            this.Quests = new ObservableCollection<Quest>();
            db = new DBRepos();
            rep = new DbDataOperation(db);
            User = user;
            Quests = rep.GetAllQuest();
        }
        
        private ObservableCollection<Quest> _quests = null;
        public ObservableCollection<Quest> Quests
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
        public Quest SelectedQuest
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
                      (openSelectQuest = new RelayCommand(obj =>new OpenSelectQuestCommand(_dialogManager, selectedQuest.Id_quest, User, db).Execute(obj),
                      (obj) => SelectedQuest != null));
            }
            
        }
        private RelayCommand createQuest;
        public RelayCommand CreateQuest
        {
            get
            {
                return createQuest ??
                      (createQuest = new RelayCommand(obj => new OpenSelectQuestCommand(_dialogManager, -1, User, db).Execute(obj)));
            }

        }
        private RelayCommand deleteSelectQuest;
        public RelayCommand DeleteSelectQuest
        {
            get
            {
                return deleteSelectQuest ??
                      (deleteSelectQuest = new RelayCommand(obj => DeleteQuest(), (obj) => SelectedQuest != null));
            }

        }

        private void DeleteQuest()
        {
            rep.DeleteQuest(selectedQuest.Id_quest);
        }




    }
}

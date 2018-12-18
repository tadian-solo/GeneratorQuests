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
        public DBRepos rep;
        private Quest selectedQuest;
        private User User;
       // private RiddleModel selectedRiddle;
          
        private readonly IDialogManager _dialogManager;
        public QuestViewModel(IDialogManager dialogManager, User user)
        {
            _dialogManager = dialogManager;
            this.Quests = new ObservableCollection<Quest>();
            rep = new DBRepos();
            User = user;
            Quests = rep.Quests.GetList();
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
                      (openSelectQuest = new RelayCommand(obj =>new OpenSelectQuestCommand(_dialogManager, selectedQuest.Id_quest, User).Execute(obj),
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

       
        
        
    }
}

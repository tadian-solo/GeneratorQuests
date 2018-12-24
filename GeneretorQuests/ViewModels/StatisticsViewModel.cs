using BLL;
using DAL;
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
    public class StatisticsViewModel:BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDialogManager _dialogManager;
        public DBRepos db;
        public DbDataOperation rep;
        public User User { get; set; }
        public ObservableCollection<Riddle> Riddls { get; set; }
        private ObservableCollection<Quest> _quests;
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
        private string year;
        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Year"));
            }
        }

        User SelUser;
        public StatisticsViewModel(IDialogManager dialogManager, User u)
        {
            _dialogManager = dialogManager;
            SelUser = u;
            db = new DBRepos();
            rep = new DbDataOperation(db);
            Riddls = rep.GetTopRiddls();
            User = rep.GetTopUser();
            Quests = new ObservableCollection<Quest>();
            //Quests = rep.GetAllQuest();
        }
        private RelayCommand openReadyQuests;
        public RelayCommand OpenReadyQuests
        {
            get
            {
                return openReadyQuests ??
                      (openReadyQuests = new RelayCommand(obj =>
                      {
                          new OpenReadyQuestsCommand(_dialogManager, SelUser).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private RelayCommand openReadyRiddls;
        public RelayCommand OpenReadyRiddls
        {
            get
            {
                return openReadyRiddls ??
                      (openReadyRiddls = new RelayCommand(obj =>
                      {
                          new OpenReadyRiddlsCommand(_dialogManager, SelUser).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private RelayCommand openMain;
        public RelayCommand OpenMain
        {
            get
            {
                return openMain ??
                      (openMain = new RelayCommand(obj =>
                      {
                          new OpenMainWindowCommand(_dialogManager, SelUser).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private ICommand getQuestsForYear;
        public ICommand GetQuestsForYear
        {
            get
            {

                return getQuestsForYear ??
                      (getQuestsForYear = new RelayCommand(obj => GetQuests(year), (obj) => Year != null));


            }
        }
        void GetQuests(string year)
        {
            Quests=rep.GetQuestsForYear(year);
        }
    }
}

using DAL;
using GeneretorQuests.ViewModels.Commands;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
    public class MyViewModel : BaseViewModel
    {
        //public Action CloseAction { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;
        //public event EventHandler<EventArgs<QuestViewModel>> openQuests;
        private readonly User SelectUser;
        private readonly IDialogManager _dialogManager;
        public MyViewModel(IDialogManager dialogManager, User user)
        {
            _dialogManager = dialogManager;
            SelectUser = user;

        }
        private RelayCommand openReadyQuests;
        public RelayCommand OpenReadyQuests
        {
            get
            {
                return openReadyQuests ??
                      (openReadyQuests = new RelayCommand(obj =>
                      {
                          new OpenReadyQuestsCommand(_dialogManager, SelectUser).Execute(obj);
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
                          new OpenReadyRiddlsCommand(_dialogManager, SelectUser).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private RelayCommand getStatistics;
        public RelayCommand GetStatistics
        {
            get
            {
                return getStatistics ??
                      (getStatistics = new RelayCommand(obj =>
                      {
                          new OpenStatisticsCommand(_dialogManager).Execute(obj);
                          CloseAction();
                      }));
            }

        }
    }

    /*public class EventArgs<T> : EventArgs
    {
        public object vm;

        public EventArgs(object vm)
        {
            this.vm = vm;
        }
    }*/
}

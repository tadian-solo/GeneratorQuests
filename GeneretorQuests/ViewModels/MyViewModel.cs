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
    public class MyViewModel 
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //public event EventHandler<EventArgs<QuestViewModel>> openQuests;
        private readonly User SelectUser;
        private readonly IDialogManager _dialogManager;
        public MyViewModel(IDialogManager dialogManager, User user)
        {
            _dialogManager = dialogManager;
            SelectUser = user;

        }
        private ICommand openReadyQuests;
        public ICommand OpenReadyQuests
        {
            get
            {
                if (openReadyQuests == null) openReadyQuests = new OpenReadyQuestsCommand(_dialogManager, SelectUser);
                return openReadyQuests;
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

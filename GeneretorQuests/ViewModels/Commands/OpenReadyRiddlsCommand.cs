using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    public class OpenReadyRiddlsCommand : ICommand
    {
        private readonly IDialogManager _dialogManager;
        private readonly User User;
        public OpenReadyRiddlsCommand(IDialogManager dialogManager, User user)
        {
            _dialogManager = dialogManager;
            User = user;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var riddls = new AllRiddlsViewModel(_dialogManager, User);
            _dialogManager.Show(riddls);


        }
    }
}

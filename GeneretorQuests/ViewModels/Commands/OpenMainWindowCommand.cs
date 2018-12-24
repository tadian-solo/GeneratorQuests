using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
   public class OpenMainWindowCommand:ICommand
    {
        private readonly IDialogManager _dialogManager;
        User User;
        public OpenMainWindowCommand(IDialogManager dialogManager, User u)
        {
            _dialogManager = dialogManager;
            User = u;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var quest = new MyViewModel(_dialogManager, User);
            _dialogManager.Show(quest);

        }
    }
}

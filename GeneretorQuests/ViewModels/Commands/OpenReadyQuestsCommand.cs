using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    public class OpenReadyQuestsCommand : ICommand
    {
        private readonly IDialogManager _dialogManager;
        public OpenReadyQuestsCommand(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var quest = new QuestViewModel(_dialogManager);
            _dialogManager.Show(quest);

        }
    }
}

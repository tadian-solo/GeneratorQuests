using GeneretorQuests.ViewModels.DTO;
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
        private readonly UserModel User;
        public OpenReadyQuestsCommand(IDialogManager dialogManager, UserModel user)
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
            var quest = new QuestViewModel(_dialogManager, User);
            _dialogManager.Show(quest);


        }
    }
}

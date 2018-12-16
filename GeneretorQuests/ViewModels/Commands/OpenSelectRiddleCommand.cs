using GeneretorQuests.Models;
using GeneretorQuests.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    public class OpenSelectRiddleCommand : ICommand
    {
        private readonly IDialogManager _dialogManager;
        private bool isNew;
        private RiddleModel selectRiddle;
        private UserModel User;
        public OpenSelectRiddleCommand(IDialogManager dialogManager, RiddleModel riddle, UserModel user, bool IsNew)
        {
            selectRiddle = riddle;
            User = user;
            isNew = IsNew;
            _dialogManager = dialogManager;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var riddle = new RiddleViewModel(selectRiddle, User, isNew);
            _dialogManager.Show(riddle);

        }
    }
}

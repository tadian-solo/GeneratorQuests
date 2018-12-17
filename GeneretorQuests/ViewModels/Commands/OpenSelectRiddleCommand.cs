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
        //private bool isNew;
        int _id;
        private RiddleModel selectRiddle;
        private UserModel User;
        public OpenSelectRiddleCommand(IDialogManager dialogManager, RiddleModel riddle, UserModel user, int id)
        {
            selectRiddle = riddle;
            User = user;
            _dialogManager = dialogManager;
            _id = id;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var riddle = new RiddleViewModel(selectRiddle, User, _id);
            _dialogManager.Show(riddle);

        }
    }
}

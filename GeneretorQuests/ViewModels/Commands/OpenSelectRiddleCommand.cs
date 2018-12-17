using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.DTO;
using System;
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
        DBRepos d;
        public OpenSelectRiddleCommand(DBRepos dd,IDialogManager dialogManager, RiddleModel riddle, UserModel user, int id)
        {
            selectRiddle = riddle;
            d = dd;
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
            var riddle = new RiddleViewModel(d,selectRiddle, User, _id);
            _dialogManager.Show(riddle);

        }
    }
}

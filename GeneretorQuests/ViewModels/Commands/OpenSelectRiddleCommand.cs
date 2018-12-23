using DAL;
using GeneretorQuests.Models.Repository;

using System;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    public class OpenSelectRiddleCommand : ICommand
    {
        private readonly IDialogManager _dialogManager;
        //private bool isNew;
        int _id;
        private Riddle selectRiddle;
        private User User;
        DBRepos d;
        bool seeAll;
        bool createOrUpdate;
        public OpenSelectRiddleCommand(DBRepos dd,IDialogManager dialogManager, Riddle riddle, User user, int id, bool SeeAll, bool IsCreate)
        {
            selectRiddle = riddle;
            d = dd;
            User = user;
            _dialogManager = dialogManager;
            _id = id;
            createOrUpdate = IsCreate;
            seeAll = SeeAll;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var riddle = new RiddleViewModel(d,selectRiddle, User, _id, seeAll, createOrUpdate);
            _dialogManager.Show(riddle);

        }
    }
}

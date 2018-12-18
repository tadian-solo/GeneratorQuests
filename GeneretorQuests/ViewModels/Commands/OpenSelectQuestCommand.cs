
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    public class OpenSelectQuestCommand : ICommand
    {//переписать команду на relay с предикатом, вызывать по другому
        private readonly IDialogManager _dialogManager;
        private User User;
        public int qm;
        public OpenSelectQuestCommand(IDialogManager dialogManager, int q, User user)
        {
            _dialogManager = dialogManager;
            qm =q;
            User = user;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
           
           return true; 
           
        }
        /*public void OnCanExecuteChanged(QuestModel qm)
        {
            //CanExecuteChanged(new OpenSelectQuestCommand(_dialogManager,qm), EventArgs.Empty); 
            //Execute(qm);
        }*/
        public void Execute(object parameter)
        {
            var quest = new SelectQuestViewModel(_dialogManager, qm, User);
            _dialogManager.Show(quest);

        }
    }
}

using GeneretorQuests.ViewModels.DTO;
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
        public QuestModel qm;
        public OpenSelectQuestCommand(IDialogManager dialogManager, QuestModel q)
        {
            _dialogManager = dialogManager;
            qm =q;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
           
            qm = parameter as QuestModel;
            if (qm != null) {/*OnCanExecuteChanged(qm); */return true; }
            else return false;
        }
        /*public void OnCanExecuteChanged(QuestModel qm)
        {
            //CanExecuteChanged(new OpenSelectQuestCommand(_dialogManager,qm), EventArgs.Empty); 
            //Execute(qm);
        }*/
        public void Execute(object parameter)
        {
            var quest = new SelectQuestViewModel(_dialogManager, qm);
            _dialogManager.Show(quest);

        }
    }
}

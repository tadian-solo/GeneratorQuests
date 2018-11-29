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
        private RiddleModel selectRiddle;
        public OpenSelectRiddleCommand(IDialogManager dialogManager, RiddleModel riddle)
        {
            selectRiddle = riddle;
            _dialogManager = dialogManager;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var riddle = new RiddleViewModel(selectRiddle);
            _dialogManager.Show(riddle);

        }
    }
}

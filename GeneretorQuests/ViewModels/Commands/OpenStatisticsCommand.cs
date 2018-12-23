using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels.Commands
{
    
    public class OpenStatisticsCommand : ICommand
    {
        private readonly IDialogManager _dialogManager;
        public OpenStatisticsCommand(IDialogManager dialogManager)
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
             var window = new StatisticsViewModel(_dialogManager);
              _dialogManager.Show(window);

        }
    }
}

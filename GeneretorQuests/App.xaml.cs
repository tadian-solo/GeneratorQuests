using GeneretorQuests.ViewModels;
using GeneretorQuests.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeneretorQuests
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // base.OnStartup(e);
            var dialog = new DialogManager();
            dialog.Register<MyViewModel, FAQ>();
            dialog.Register<QuestViewModel, ReadyQuests>();
            dialog.Register<SelectQuestViewModel, SelectQuest>();
            dialog.Register<RiddleViewModel, Riddle>();
            dialog.Register<AllRiddlsViewModel, Riddls>();
            dialog.Register<StatisticsViewModel, Statistics>();
            /*var mainWindow = new FAQ()
            {
                DataContext = new MyViewModel(dialog)
            };
            mainWindow.Show();*/
            var vm = new UserViewModel(dialog);
            var mainWindow = new User()
            {
                DataContext = vm
            };
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(mainWindow.Close);
            mainWindow.Show();
        }
    }
}

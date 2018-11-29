using GeneretorQuests.ViewModels;
using GeneretorQuests.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneretorQuests
{
    /// <summary>
    /// Логика взаимодействия для ReadyQuests.xaml
    /// </summary>
    public partial class ReadyQuests : UserControl
    {
        public ReadyQuests()
        {
            
            InitializeComponent();
            
            //QuestViewModel v = new QuestViewModel();
            //this.DataContext = new QuestViewModel();
        }
        /*private static void OpenView()
        {
            ReadyQuests w = new ReadyQuests();
            w.Show();
        }*/
    }
}

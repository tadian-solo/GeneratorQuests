using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BLL;
using DAL;
using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;


namespace GeneretorQuests.ViewModels
{
   public class UserViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DBRepos rep;
        private readonly IDialogManager _dialogManager;
        private readonly RelayCommand _loginCommand;
        private readonly RelayCommand _logoutCommand;
        public UserViewModel(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
            rep = new DBRepos();
            _loginCommand = new RelayCommand(LogIn, i => true);
            _logoutCommand = new RelayCommand(Logout);
        }
        public RelayCommand LoginCommand { get { return _loginCommand; } }

        public RelayCommand LogoutCommand { get { return _logoutCommand; } }

        private void LogIn(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
           
            selectedUser= rep.Users.GetItemForLogin(Login);
            if (clearTextPassword == selectedUser.Password)
            {
                var window = new MyViewModel(_dialogManager, selectedUser);
                _dialogManager.Show(window);
            }
           
        }
        private void Logout(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            DbDataOperation db = new DbDataOperation(rep);
            User u = new User { Name = Login, Password = clearTextPassword, Access_level = false};
            db.CreateUser(u);
           

        }
        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; this.PropertyChanged(this, new PropertyChangedEventArgs("Login")); }
        }
        
       private User selectedUser;
        
     
    }
}

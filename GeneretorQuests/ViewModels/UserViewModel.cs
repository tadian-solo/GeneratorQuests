using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;
using GeneretorQuests.ViewModels.DTO;

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
          //  _logoutCommand = new RelayCommand(Logout);
        }
        public RelayCommand LoginCommand { get { return _loginCommand; } }

        public RelayCommand LogoutCommand { get { return _logoutCommand; } }
        private void LogIn(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
           
           SelectedUser= rep.Users.GetItemForLogin(Login);
            if (clearTextPassword == SelectedUser.Password)
            {
                var window = new MyViewModel(_dialogManager, UserToModel(SelectedUser));
                _dialogManager.Show(window);
            }
            
      
            
        }

        private UserModel UserToModel(User selectedUser)
        {
            UserModel user = new UserModel
            {
                Id_user = selectedUser.Id_user,
                Name = selectedUser.Name,
                Password = selectedUser.Password,
                Access_level = selectedUser.Access_level
            };
            return user;
        }

        private User selectedUser;
        private string login;
        //private string password;
        public string Login
        {
            get { return login; }
            set { login = value; this.PropertyChanged(this, new PropertyChangedEventArgs("Login")); }
        }
     /*   public string Password
        {
            get { return password; }
            set { password = value; this.PropertyChanged(this, new PropertyChangedEventArgs("Password")); }
        }*/
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                


            }

        }
      /*  private ICommand openMainWindow;
        public ICommand OpenMainWindow
        {
            
            get
            {
                return openMainWindow ??
                      (openMainWindow = new RelayCommand(obj => new OpenMainWindowCommand(_dialogManager, SelectedUser).Execute(obj),
                      (obj) => SelectedUser != null));
            }
        }*/
    }
}

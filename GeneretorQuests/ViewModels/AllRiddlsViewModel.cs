﻿using BLL;
using DAL;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
    public class AllRiddlsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public Action CloseAction { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDialogManager _dialogManager;
        public DbDataOperation rep;
        private Riddle selectedRiddle;
        private User User;
        private RelayCommand openReadyQuests;
        public RelayCommand OpenReadyQuests
        {
            get
            {
                return openReadyQuests ??
                      (openReadyQuests = new RelayCommand(obj =>
                      {
                          new OpenReadyQuestsCommand(_dialogManager, User).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private RelayCommand openMain;
        public RelayCommand OpenMain
        {
            get
            {
                return openMain ??
                      (openMain = new RelayCommand(obj =>
                      {
                          new OpenMainWindowCommand(_dialogManager, User).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private RelayCommand getStatistics;
        public RelayCommand GetStatistics
        {
            get
            {
                return getStatistics ??
                      (getStatistics = new RelayCommand(obj =>
                      {
                          new OpenStatisticsCommand(_dialogManager, User).Execute(obj);
                          CloseAction();
                      }));
            }

        }
        private ObservableCollection<Riddle> riddls = null;
        public ObservableCollection<Riddle> Riddls
        {
            get { return riddls; }
            set
            {
                if (riddls != value)
                {
                    riddls = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Riddls"));
                    }
                }
            }
        }
        public Riddle SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
            }

        }
      /*  private bool added;
        public bool Added
        {
            get { return added; }
            set
            {
                added = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("Added"));
            }
        }*/
        private ICommand openSelectRiddle;
        public ICommand OpenSelectRiddle
        {
            get
            {
                return openSelectRiddle ??
                      (openSelectRiddle = new RelayCommand(obj =>
                      {
                          new OpenSelectRiddleCommand(d, _dialogManager, selectedRiddle, User, -1, true, false).Execute(obj);
                          //CloseAction();
                      },
                      (obj) => SelectedRiddle != null));

            }
        }
        private ICommand createRiddle;
        public ICommand CreateRiddle
        {
            get
            {
                return createRiddle ??
                      (createRiddle = new RelayCommand(obj =>
                      {
                          new OpenSelectRiddleCommand(d, _dialogManager, null, User, -1, true, true).Execute(obj);
                          //CloseAction();
                      }));

            }
        }
        private ICommand deleteSelectRiddle;
        public ICommand DeleteSelectRiddle//delete realy hehe
        {
            get
            {
                return deleteSelectRiddle ??
                      (deleteSelectRiddle = new RelayCommand(obj => DeleteRiddle(),
                      (obj) => SelectedRiddle != null));

            }
        }
        void DeleteRiddle()
        {
            rep.DeleteRiddle(selectedRiddle.Id_riddle);
            
            

        }
        /*private ICommand addRiddle;
        public ICommand AddRiddle
        {
            get
            {
                return addRiddle ??
                      (addRiddle = new RelayCommand(obj => new OpenSelectRiddleCommand(d, _dialogManager, null, User, -1).Execute(obj)));

            }
        }*/
        
        public DBRepos d;
        public AllRiddlsViewModel(IDialogManager dm,  User user)
        {
            _dialogManager = dm;
            d= new DBRepos();
            User = user;
            rep = new DbDataOperation(d);
            Riddls = rep.GetAllRiddle();

        }

       
    }
}

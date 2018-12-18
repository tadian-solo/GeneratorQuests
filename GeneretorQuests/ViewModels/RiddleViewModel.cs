using BLL;
using DAL;
using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;

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
    public class RiddleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DbDataOperation rep;

        public ObservableCollection<Level_of_complexity> Levels { get; set; }
        private ObservableCollection<Type_of_question> types;
        public ObservableCollection<Type_of_question> Types
        {
            get { return types; }
            set
            {
                types = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Types"));
            }
        }
        ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Answers"));
            }
        }
        Answer newAnswer;
        public Answer NewAnswer
        {
            get { return newAnswer; }
            set
            {
                newAnswer = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("NewAnswer"));
            }

        }
        bool isNewChecked;
      
        public bool IsNewChecked
           {
            get { return isNewChecked; }
            set
            {
                isNewChecked = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("IsNewChecked"));
                if (isNewChecked)
                {
                    Levels = rep.GetAllLevel();
                    Types = rep.GetAllType();
                    Answers = rep.GetAllAnswer();
                }
           }
        }

        private User User;
        private Riddle selectedRiddle;
        
        public Riddle SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
                

            }

        }
       
        private ICommand addNewAnswer;
        public ICommand AddNewAnswer
        {
            get
            {
                
                return addNewAnswer ??
                      (addNewAnswer = new RelayCommand(obj => OnAddAnswer(NewAnswer),
                      (obj) => NewAnswer.Object != null));
                

            }
        }
        private ICommand saveRiddle;
        public ICommand SaveRiddle
        {
            get
            {
                
                return saveRiddle ??
                      (saveRiddle = new RelayCommand(obj =>
                      {
                          
                          if (isNew)
                          {
                              var q = rep.GetQuest(quest_id);
                              q.Riddle.Add(selectedRiddle);
                              rep.UpdateQuest(q);
                          }
                          
                          rep.SaveRiddle(User.Access_level || (User.Id_user == selectedRiddle.Id_Autor_FK), IsNewChecked, selectedRiddle);
                      }
                      ));


            }
        }
        void OnAddAnswer(Answer a)
        {
            
            rep.CreateAnswer(a);
            IsNewChecked = true;
            //Answers = rep.Answers.GetList();
        }
        int quest_id;
        bool isNew;
        public RiddleViewModel(DBRepos d, Riddle r, User user, int id, bool IsCreated)
        {
            rep = new DbDataOperation(d);
            quest_id = id;
            isNew = IsCreated;
            selectedRiddle = r??new Riddle();
            isNewChecked = false;
            User = user;
            Levels = new ObservableCollection<Level_of_complexity>();
            Levels = rep.GetAllLevel();
            Types = new ObservableCollection<Type_of_question>();
            Types = rep.GetAllType();
            Answers = new ObservableCollection<Answer>();
            Answers = rep.GetAllAnswer();
            newAnswer = new Answer();
            //selectedRiddle.levelChangeEvent +=OnLevelChange;
            //selectedRiddle.typeChangeEvent += OnTypeChange;
            //selectedRiddle.answerChangeEvent += OnAnswerChange;

        }

        private void OnLevelChange(int id)
        {
            if (!isNewChecked)
            {
                Types = rep.GetListTypeForLevel(id);
                if (Types.Count != 0) selectedRiddle.Id_Type_FK = Types[0].Id_type;
                else selectedRiddle.Id_Type_FK = -1;
            }
            //else Types = rep.Types.GetList(); 
        }
        private void OnTypeChange(int id, int level)
        {
            if(!isNewChecked)
          {
            Answers = rep.GetListAnswerForType(id, level);
            if (Answers.Count!= 0) selectedRiddle.Id_Answer_FK = Answers[0].Id_answer;
            else selectedRiddle.Id_Answer_FK = -1;
          }
           // else Answers = rep.Answers.GetList();

        }
        private void OnAnswerChange(int id, int level, int type)
        {
           /* if (!isNewChecked)
            {
                RiddleModel r = toRiddleModel(rep.GetAllRiddle().Where(i => i.Id_Level_FK == level && i.Id_Type_FK == type && i.Id_Answer_FK == id).FirstOrDefault());
                selectedRiddle.Text = r.Text; selectedRiddle.Autor_name = r.Autor_name; selectedRiddle.Description = r.Description; selectedRiddle.Status = r.Status; selectedRiddle.Quest = r.Quest;
                selectedRiddle.Level_name = r.Level_name; selectedRiddle.Id_riddle = r.Id_riddle; selectedRiddle.Id_Autor_FK = r.Id_Autor_FK; selectedRiddle.Type_name = r.Type_name;  selectedRiddle.Answer_name = r.Answer_name;
               
                // SelectedRiddle = r;
               
                
            }*/
            

        }
        
    }
}

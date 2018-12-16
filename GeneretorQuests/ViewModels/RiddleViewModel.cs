using GeneretorQuests.Models;
using GeneretorQuests.Models.Repository;
using GeneretorQuests.ViewModels.DTO;
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
        public DBRepos rep;

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
                    OnLevelChange(-1);
                    OnTypeChange(-1, -1);
                }
            }
        }

        private UserModel User;
        private RiddleModel selectedRiddle;
        
        public RiddleModel SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                //OnLevelChange(selectedRiddle.Id_Level_FK);
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
                

            }

        }
        /*private int selectedLevel;

        public int SelectedLevel
        {
            get { return selectedLevel; }
            set
            {
                selectedLevel = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedLevel"));
            }

        }*/
        
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
                      (saveRiddle = new RelayCommand(obj => rep.Riddls.Save(User.Access_level||(User.Id_user==selectedRiddle.Id_Autor_FK), IsNewChecked, toRiddle(selectedRiddle))));


            }
        }
        void OnAddAnswer(Answer a)
        {
            isNewChecked = true;
            rep.Answers.Create(a);
            Answers = rep.Answers.GetList();
        }
        bool isNewAdd;//!!
        public RiddleViewModel( RiddleModel r, UserModel user, bool isNew)
        {
            rep = new DBRepos();
            selectedRiddle = r;
            isNewChecked = false;
            isNewAdd = isNew;
            /*selectedLevel = selectedRiddle.Id_Level_FK;*/
            User = user;
            Levels = new ObservableCollection<Level_of_complexity>();
            Levels = rep.Levels.GetList();
            Types = new ObservableCollection<Type_of_question>();
            Types = rep.Types.GetList();
            Answers = new ObservableCollection<Answer>();
            Answers = rep.Answers.GetList();
            newAnswer = new Answer();
            selectedRiddle.levelChangeEvent +=OnLevelChange;
            selectedRiddle.typeChangeEvent += OnTypeChange;
            selectedRiddle.answerChangeEvent += OnAnswerChange;

        }

        private void OnLevelChange(int id)
        {
            if (!isNewChecked)
            {
                Types = rep.Types.GetListForLevel(id);
                if (Types.Count != 0) selectedRiddle.Id_Type_FK = Types[0].Id_type;
                else selectedRiddle.Id_Type_FK = -1;
            }
            else Types = rep.Types.GetList(); 
        }
        private void OnTypeChange(int id, int level)
        {
            if(!isNewChecked)
          {
            Answers = rep.Answers.GetListForType(id, level);
            if (Answers.Count!= 0) selectedRiddle.Id_Answer_FK = Answers[0].Id_answer;
            else selectedRiddle.Id_Answer_FK = -1;
          }
            else Answers = rep.Answers.GetList();

        }
        private void OnAnswerChange(int id, int level, int type)
        {
            if (!isNewChecked)
            {
                RiddleModel r = toRiddleModel(rep.Riddls.GetList().Where(i => i.Id_Level_FK == level && i.Id_Type_FK == type && i.Id_Answer_FK == id).FirstOrDefault());
                selectedRiddle.Text = r.Text; //selectedRiddle.Autor_name = r.Autor_name;
                //SelectedRiddle = r;
               
                
            }
            

        }
        private RiddleModel toRiddleModel(Riddle i)
        {
            return new RiddleModel()
            {
                Id_riddle = i.Id_riddle,
                Text = i.Text,
                Description = i.Description,
                Status = i.Status,
                Id_Autor_FK = i.Id_Autor_FK,
                Autor_name = i.User.Name,
                Id_Level_FK = i.Id_Level_FK,
                Level_name = i.Level_of_complexity.Name_level,
                Id_Answer_FK = i.Id_Answer_FK,
                Answer_name = i.Answer.Object,
                Id_Type_FK = i.Id_Type_FK,
                Type_name = i.Type_of_question.Name,
                //Image
            };
        }
        private Riddle toRiddle(RiddleModel i)
        {
            return new Riddle()
            {
                Id_riddle = i.Id_riddle,
                Text = i.Text,
                Description = i.Description,
                Status = i.Status,
                Id_Autor_FK = i.Id_Autor_FK,//selected?
                User = rep.Users.GetItem(i.Id_Autor_FK),
                Id_Level_FK = i.Id_Level_FK,
                Level_of_complexity = rep.Levels.GetItem(i.Id_Level_FK),
                Id_Answer_FK = i.Id_Answer_FK,
                Answer = rep.Answers.GetItem(i.Id_Answer_FK),
                Id_Type_FK = i.Id_Type_FK,
                Type_of_question = rep.Types.GetItem(i.Id_Type_FK),
                //Quest=rep.Quests.GetList().Where(i=>i.Riddle.Contains())
                //Image
            };
        }
    }
}

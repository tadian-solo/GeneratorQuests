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
    public interface ISave
    {
        void Save(Riddle r, int questId);
    }
    public class CreateAndSave : ISave
    {
        DbDataOperation rep;
        public CreateAndSave(DbDataOperation r)
        {
            rep = r;
        }
        public void Save(Riddle t, int questId)
        {
            var r = new Riddle
            {
                Text = t.Text,
                Description = t.Description,
                Status = t.Status,
                Id_Autor_FK = t.Id_Autor_FK,
                Id_Level_FK = t.Id_Level_FK,
                Id_Answer_FK = t.Id_Answer_FK,
                Id_Type_FK = t.Id_Type_FK,
                Answer = t.Answer,
                Level_of_complexity = t.Level_of_complexity,
                Type_of_question = t.Type_of_question,
                User = t.User,
                Quest = t.Quest
            };
            if (questId!=-1)
            {

                var q = rep.GetQuest(questId);
                r.Quest.Add(q);
                //q.Riddle.Add(r);
                rep.UpdateQuest(q);
            }
            rep.CreateRiddle(r);
        }
    }
    public class UpdateAndSave : ISave
    {
        DbDataOperation rep;
        public UpdateAndSave(DbDataOperation r)
        {
            rep = r;
        }
        public void Save(Riddle r, int questId)
        {
            if (questId != -1)
            {

                var q = rep.GetQuest(questId);
                r.Quest.Add(q);
                //q.Riddle.Add(r);
                rep.UpdateQuest(q);
            }
            rep.UpdateRiddle(r);
        }
    }
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
        Level_of_complexity selectedLevel;
        public Level_of_complexity SelectedLevel
        {
            get { return selectedLevel; }
            set
            {
                selectedLevel = value;
                OnLevelChange(selectedLevel.Id_level);
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedLevel"));
            }

        }
       Type_of_question selectedType;
        public Type_of_question SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnTypeChange(selectedType.Id_type, selectedLevel.Id_level);
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedType"));
            }

        }
        Answer selectedAnswer;
        public Answer SelectedAnswer
        {
            get { return selectedAnswer; }
            set
            {
                selectedAnswer = value;
                OnAnswerChange(selectedAnswer.Id_answer, selectedLevel.Id_level, selectedType.Id_type);
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedAnswer"));
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
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
                

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

                          if ((User.Access_level || (User.Id_user == selectedRiddle.Id_Autor_FK)) && !isNewChecked) save = new UpdateAndSave(rep);
                          else save = new CreateAndSave(rep);
                          save.Save(selectedRiddle, quest_id);
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
        ISave save;
        bool isNew;//добавляется в квест или просто создается
        public RiddleViewModel(DBRepos d, Riddle r, User user, int id, bool IsCreated)
        {
            rep = new DbDataOperation(d);
            quest_id = id;
            isNew = IsCreated;
            selectedRiddle = r??new Riddle();//
            isNewChecked = false;
            User = user;
            Levels = new ObservableCollection<Level_of_complexity>();
            Levels = rep.GetAllLevel();
            Types = new ObservableCollection<Type_of_question>();
            Types = rep.GetAllType();
            Answers = new ObservableCollection<Answer>();
            Answers = rep.GetAllAnswer();
            newAnswer = new Answer();
            SelectedLevel = SelectedRiddle.Level_of_complexity??Levels[0];
            SelectedType = SelectedRiddle.Type_of_question??Types[0];
            SelectedAnswer = SelectedRiddle.Answer??Answers[0];

        }

        private void OnLevelChange(int id)
        {
            if (!isNewChecked)
            {
                Types = rep.GetListTypeForLevel(id);
                if (Types.Count != 0) selectedRiddle.Type_of_question = Types[0];
                else { selectedRiddle.Id_Type_FK = -1; selectedRiddle.Id_Answer_FK = -1; }
            }
            //else Types = rep.Types.GetList(); 
        }
        private void OnTypeChange(int id, int level)
        {
            if(!isNewChecked)
          {
            Answers = rep.GetListAnswerForType(id, level);
            if (Answers.Count!= 0) selectedRiddle.Answer = Answers[0];
            else selectedRiddle.Id_Answer_FK = -1;
          }
           // else Answers = rep.Answers.GetList();

        }
        private void OnAnswerChange(int id, int level, int type)
        {
            if (!isNewChecked)
            {
                Riddle r = rep.GetAllRiddle().Where(i => i.Id_Level_FK == level && i.Id_Type_FK == type && i.Id_Answer_FK == id).FirstOrDefault();
                /*selectedRiddle.Text = r.Text; selectedRiddle.User=r.User; selectedRiddle.Description = r.Description; selectedRiddle.Status = r.Status; selectedRiddle.Quest = r.Quest;
                selectedRiddle.Level_of_complexity = r.Level_of_complexity; selectedRiddle.Id_riddle = r.Id_riddle; selectedRiddle.Id_Autor_FK = r.Id_Autor_FK; selectedRiddle.Type_of_question = r.Type_of_question;  selectedRiddle.Answer = r.Answer;
               */
                SelectedRiddle = r;
               
                
            }
            else
            {
                selectedRiddle.Level_of_complexity = SelectedLevel; selectedRiddle.Id_Level_FK = SelectedLevel.Id_level;
                selectedRiddle.Type_of_question = selectedType; selectedRiddle.Id_Type_FK = SelectedType.Id_type;
                selectedRiddle.Answer = selectedAnswer; selectedRiddle.Id_Answer_FK = SelectedAnswer.Id_answer;
            }

        }
        
    }
}

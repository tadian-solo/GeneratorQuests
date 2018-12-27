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
using System.Windows;
using System.Windows.Input;

namespace GeneretorQuests.ViewModels
{
    public interface ISave
    {
        void Save(Riddle r, int questId, User User);
    }
    public class CreateAndSave : ISave
    {
        DbDataOperation rep;
        public CreateAndSave(DbDataOperation r)
        {
            rep = r;
        }
        public void Save(Riddle t, int questId, User User)
        {
            var r = new Riddle
            {
                Text = t.Text,
                Description = t.Description,
                Status = t.Status,
                Id_Autor_FK = User.Id_user,
                Id_Level_FK = t.Id_Level_FK,
                Id_Answer_FK = t.Id_Answer_FK,
                Id_Type_FK = t.Id_Type_FK,
                Answer = t.Answer,
                Level_of_complexity = t.Level_of_complexity,
                Type_of_question = t.Type_of_question,
                //User = User,
                Quest = t.Quest
            };
            rep.DetachRiddle(t);
            if (questId!=-1)
            {

                var q = rep.GetQuest(questId);
                r.Quest.Add(q);
                //q.Riddle.Add(r);
                rep.CreateRiddle(r);
                rep.UpdateQuest(q);
            }
            else rep.CreateRiddle(r);
        }
    }
    public class UpdateAndSave : ISave
    {
        DbDataOperation rep;
        public UpdateAndSave(DbDataOperation r)
        {
            rep = r;
        }
        public void Save(Riddle r, int questId, User User)
        {
            if (questId != -1)
            {

                var q = rep.GetQuest(questId);
                r.Quest.Add(q);
                //q.Riddle.Add(r);
                rep.UpdateRiddle(r);
                rep.UpdateQuest(q);//
            }
           else  rep.UpdateRiddle(r);
        }
    }
    public class RiddleViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public Action CloseAction { get; set; }
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
                    seeAll = true;
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
                          //OnAnswerChange(selectedAnswer.Id_answer, selectedLevel.Id_level, selectedType.Id_type);
                          if (verify())
                          {
                              OnAnswerChange(selectedAnswer.Id_answer, selectedLevel.Id_level, selectedType.Id_type);
                              if (/*(User.Access_level || (User.Id_user == selectedRiddle.Id_Autor_FK)) && */!isNewChecked) save = new UpdateAndSave(rep);
                              else save = new CreateAndSave(rep);
                              save.Save(selectedRiddle, quest_id, User);
                              CloseAction();
                          }
                          else MessageBox.Show("Пожалуйста, заполните все поля");
                      }
                      ));


            }
        }
        bool verify()
        {
            if (selectedRiddle.Level_of_complexity == null || selectedRiddle.Answer == null || selectedRiddle.Text == null || selectedRiddle.Type_of_question == null) return false;
            else return true;
        }
        void OnAddAnswer(Answer a)
        {
            
            rep.CreateAnswer(a);
            IsNewChecked = true;
            //Answers = rep.Answers.GetList();
        }
        int quest_id;
        ISave save;
        bool seeAll;

        public RiddleViewModel(DBRepos d, Riddle r, User user, int id, bool SeeAll, bool CreateOrUpdate)
        {
            rep = new DbDataOperation(d);
            quest_id = id;
            Levels = new ObservableCollection<Level_of_complexity>();
            Levels = rep.GetAllLevel();
            Types = new ObservableCollection<Type_of_question>();
            Types = rep.GetAllType();
            Answers = new ObservableCollection<Answer>();
            Answers = rep.GetAllAnswer();
            seeAll = SeeAll;
            isNewChecked = CreateOrUpdate;
            selectedRiddle = r??new Riddle();//
            if (!CreateOrUpdate&&SeeAll)
            {
              SelectedLevel = r.Level_of_complexity;
              SelectedType = r.Type_of_question;
              SelectedAnswer = r.Answer;
            }
            else
            {
                SelectedLevel = Levels[0];
            }
           
            User = user;
            newAnswer = new Answer();
            

        }

        private void OnLevelChange(int id)
        {
            if (!seeAll)
            {
                Types = rep.GetListTypeForLevel(id);
                if (Types.Count != 0) SelectedType = Types[0];
                else { SelectedType=null; SelectedAnswer =null; }
                OnTypeChange(SelectedType.Id_type, SelectedLevel.Id_level);
            }
            //else Types = rep.Types.GetList(); 
        }
        private void OnTypeChange(int id, int level)
        {
            if(!seeAll)
          {
            Answers = rep.GetListAnswerForType(id, level);
            if (Answers.Count!= 0) SelectedAnswer = Answers[0];
            else SelectedAnswer =null;
          }
           // else Answers = rep.Answers.GetList();

        }
        private void OnAnswerChange(int id, int level, int type)
        {
            if (!seeAll)
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

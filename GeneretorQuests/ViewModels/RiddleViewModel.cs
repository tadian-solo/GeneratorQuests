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
        private UserModel User;
        private RiddleModel selectedRiddle;
        
        public RiddleModel SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
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
        
        public RiddleViewModel( RiddleModel r, UserModel user)
        {
            rep = new DBRepos();
            selectedRiddle = r;
            /*selectedLevel = selectedRiddle.Id_Level_FK;*/
            User = user;
            Levels = new ObservableCollection<Level_of_complexity>();
            Levels = rep.Levels.GetList();
            Types = new ObservableCollection<Type_of_question>();
            Types = rep.Types.GetList();
            Answers = new ObservableCollection<Answer>();
            Answers = rep.Answers.GetList();
            selectedRiddle.levelChangeEvent +=OnLevelChange;
            selectedRiddle.typeChangeEvent += OnTypeChange;
        }

        private void OnLevelChange(int id)
        {
            Types =rep.Types.GetListForLevel(id);
            selectedRiddle.Id_Type_FK = Types[0].Id_type;
        }
        private void OnTypeChange(int id, int level)
        {
            Answers = rep.Answers.GetListForType(id, level);
            selectedRiddle.Id_Answer_FK = Answers[0].Id_answer;
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

    }
}

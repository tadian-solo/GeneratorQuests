﻿using GeneretorQuests.Models;
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
   public class SelectQuestViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DBRepos rep;
        private QuestModel selectedQuest;
        private RiddleModel selectedRiddle;
        public QuestModel SelectedQuest
        {
            get { return selectedQuest; }
            set
            {
                selectedQuest = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedQuest"));
            }

        }
        public RiddleModel SelectedRiddle
        {
            get { return selectedRiddle; }
            set
            {
                selectedRiddle = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedRiddle"));
            }

        }
        public SelectQuestViewModel()
        {
            
        }

        
        private ObservableCollection<RiddleModel> GetListForQuest(int Id_quest)
        {
            // ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetList().SelectMany(u=>u.Quest, (u, q)=>new { Riddle=u, Quest=q}).Where(u=>u.Quest.Id_quest== Id_quest).Select(i => toRiddleModel(i)).ToList());
            ObservableCollection<RiddleModel> riddls = new ObservableCollection<RiddleModel>(rep.Riddls.GetListForQuest(Id_quest).Select(i => toRiddleModel(i)).ToList());
            return riddls;
        }
        private QuestModel toQuestModel(Quest i)
        {
            return new QuestModel()
            {
                Id_quest = i.Id_quest,
                Status = i.Status,
                Number_of_questions = i.Number_of_questions,
                Thematics = i.Thematics,
                Id_Level_FK = i.Id_Level_FK,
                Id_Autor_FK = i.Id_Autor_FK,
                Autor_name = i.User.Name,
                Date = i.Date,
                Level_name = i.Level_of_complexity.Name_level,
                Riddle = GetListForQuest(i.Id_quest)
            };
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
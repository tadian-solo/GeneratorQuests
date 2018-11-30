using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.ViewModels.DTO
{
    public class QuestModel
    {

        public int Id_quest { get; set; }

        public bool Status { get; set; }

        public int Number_of_questions { get; set; }


        public string Thematics { get; set; }

        public int Id_Level_FK { get; set; }
        public string Level_name { get; set; }
        public int Id_Autor_FK { get; set; }
        public string Autor_name { get; set; }

        public DateTime Date { get; set; }
        public ObservableCollection<RiddleModel> Riddle { get; set; }

       

        // public virtual Level_of_complexity Level_of_complexity { get; set; }

        // public virtual User User { get; set; }


    }
}

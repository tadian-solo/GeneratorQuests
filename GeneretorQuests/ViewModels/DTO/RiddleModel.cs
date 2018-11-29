using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.ViewModels.DTO
{
    public class RiddleModel
    {

        public int Id_riddle { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int Id_Autor_FK { get; set; }
        public string Autor_name { get; set; }

        public int Id_Level_FK { get; set; }
        public string Level_name { get; set; }
        public int Id_Answer_FK { get; set; }
        public string Answer_name { get; set; }
        public int Id_Type_FK { get; set; }
        public string Type_name { get; set; }
        public int? Id_Image_FK { get; set; }

        // public virtual Answer Answer { get; set; }

        //public virtual Image Image { get; set; }

        //public virtual Level_of_complexity Level_of_complexity { get; set; }

        //public virtual Type_of_question Type_of_question { get; set; }

        // public virtual User User { get; set; }


        //public virtual ICollection<Quest> Quest { get; set; }
    }
}

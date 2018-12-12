using GeneretorQuests.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.ViewModels.DTO
{
    public class RiddleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void LevelChange(int id);
        public event LevelChange levelChangeEvent;
        public delegate void TypeChange(int id, int level);
        public event TypeChange typeChangeEvent;
        public int Id_riddle { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int Id_Autor_FK { get; set; }
        public string Autor_name { get; set; }
        private int id_Level_FK;
        public int Id_Level_FK
        {
            get { return id_Level_FK; }
            set
            {
                id_Level_FK = value;
                if (this.PropertyChanged != null) OnPropertyChanged("Id_Level_FK");
            }
        }
        public string Level_name { get; set; }
        private int id_Answer_FK;
        public int Id_Answer_FK
        {
            get { return id_Answer_FK; }
            set
            {
                id_Answer_FK = value;
                if (this.PropertyChanged != null) OnPropertyChanged("Id_Answer_FK");
            }
        }
        public string Answer_name { get; set; }
        private int id_Type_FK;
        public int Id_Type_FK
        {
            get { return id_Type_FK; }
            set
            {
                id_Type_FK = value;
                if (this.PropertyChanged != null) OnPropertyChanged("Id_Type_FK");
            }
        }
        public string Type_name { get; set; }
        public int? Id_Image_FK { get; set; }

        

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "Id_Level_FK") { levelChangeEvent(id_Level_FK); typeChangeEvent(id_Type_FK, id_Level_FK); }
            
            if (propertyName == "Id_Type_FK") typeChangeEvent(id_Type_FK, id_Level_FK);
        }

        // public virtual Answer Answer { get; set; }

        //public virtual Image Image { get; set; }

        //public virtual Level_of_complexity Level_of_complexity { get; set; }

        //public virtual Type_of_question Type_of_question { get; set; }

        // public virtual User User { get; set; }


        //public virtual ICollection<Quest> Quest { get; set; }
    }
}

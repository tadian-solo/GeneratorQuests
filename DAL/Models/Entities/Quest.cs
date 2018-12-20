namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    [Table("Quest")]
    public partial class Quest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quest()
        {
            Riddle = new HashSet<Riddle>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        [Key]
        public int Id_quest { get; set; }
        private bool status;
        public bool Status
        {
            get { return status; }
            set
            {
                status = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        int number_of_questions;
        public int Number_of_questions
        {
            get { return number_of_questions; }
            set
            {
                number_of_questions = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Number_of_questions"));
            }
        }
        string thematics;
        [Column(TypeName = "text")]
        public string Thematics
        {
            get { return thematics; }
            set
            {
                thematics = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Thematics"));
            }
        }
        public int Id_Level_FK { get; set; }

        public int Id_Autor_FK { get; set; }

        public DateTime Date { get; set; }

        public virtual Level_of_complexity Level_of_complexity { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}

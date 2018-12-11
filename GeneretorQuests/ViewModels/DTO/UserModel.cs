using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.ViewModels.DTO
{
  public class UserModel
    {
    
        public int Id_user { get; set; }
        
        public string Name { get; set; }

        public string Password { get; set; }

        public bool Access_level { get; set; }

       // public virtual ICollection<QuestModel> Quest { get; set; }

    }
}

using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.Models.Repository
{
    public class RiddleRepos
    {
        private GQ db;
      
        
        public RiddleRepos(GQ dbContext)
        {
            db = dbContext;
            
            db.Riddle.Include("Level_of_complexity").Include("Image").Include("Type_of_question").Include("User").Include("Answer").Load();
        }

        public void Create(Riddle item)
        {

            /*var r = new Riddle
            {
                Text = item.Text,
                Description = item.Description,
                Status = item.Status,
                Id_Autor_FK = item.Id_Autor_FK,
                Id_Level_FK = item.Id_Level_FK,
                Id_Answer_FK = item.Id_Answer_FK,
                Id_Type_FK = item.Id_Type_FK,
                Answer = item.Answer,
                Level_of_complexity = item.Level_of_complexity,
                Type_of_question = item.Type_of_question,
                User = item.User,
                Quest = item.Quest
            };*/
            db.Riddle.Add(item);
            
        }

        public void Delete(int id)
        {
            Riddle k = db.Riddle.Find(id);
            if (k != null) db.Riddle.Remove(k);
            
        }

        public Riddle GetItem(int id)
        {
            return db.Riddle.Find(id);
        }

        public ObservableCollection<Riddle> GetList()
        {
            return db.Riddle.Local;
        }

        public void Update(Riddle item)
        {
            /*db.SaveChanges();
            //db.Entry(item).State = EntityState.Modified;
            var local = db.Set<Riddle>()
                    .Local
                    .FirstOrDefault(f => f.Id_riddle == item.Id_riddle);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }*/
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            

        }

        public ObservableCollection<Riddle> GetListForQuest(int id)
        {
            Quest q = db.Quest.Find(id);
            ObservableCollection<Riddle> rid = new ObservableCollection<Riddle>();
            foreach (var t in q.Riddle) rid.Add(db.Riddle.Find(t.Id_riddle));
            return rid;
        }
        /*public string GenerDescription()
        {
            
            return descriptions[r.Next(0, descriptions.Count)];

        }*/

        /*public void Save(bool access_level, bool isNewChecked, Riddle r)
        {
            if (access_level && !isNewChecked)  Update(r);
            else Create(r);
        }*/
    }
}

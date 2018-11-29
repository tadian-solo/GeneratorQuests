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
            db.Entry(item).State = EntityState.Modified;
        }

        public ObservableCollection<Riddle> GetListForQuest(int id)
        {
            Quest q = db.Quest.Find(id);
            ObservableCollection<Riddle> rid = new ObservableCollection<Riddle>();
            foreach (var t in q.Riddle) rid.Add(db.Riddle.Find(t.Id_riddle));
            return rid;
        }
    }
}

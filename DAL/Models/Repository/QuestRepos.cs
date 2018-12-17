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
    public class QuestRepos
    {
        private GQ db;
        public QuestRepos(GQ dbContext)
        {
            db = dbContext;
            db.Quest.Include("Level_of_complexity").Include("Riddle").Include("User").Load();
        }

        public void Create(Quest item)
        {
            db.Quest.Add(item);
        }

        public void Delete(int id)
        {
            Quest k = db.Quest.Find(id);
            if (k != null) db.Quest.Remove(k);
        }

        public Quest GetItem(int id)
        {
            return db.Quest.Find(id);
        }

        public ObservableCollection<Quest> GetList()
        {
            return db.Quest.Local;
        }

        public void Update(Quest item)
        {
            ICollection<Riddle> rs = item.Riddle;
            foreach (var t in rs) { Riddle rt = db.Riddle.Find(t.Id_riddle); db.Entry(rt).State = EntityState.Detached; }
            User u = db.User.Find(item.Id_Autor_FK);
            Quest r = db.Quest.Find(item.Id_quest);
            db.Entry(r).State = EntityState.Detached;
            db.Entry(u).State = EntityState.Detached;
            db.Entry(item).State = EntityState.Modified; 
            db.SaveChanges();
        }
    }
}

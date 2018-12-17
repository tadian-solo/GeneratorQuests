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
        Random r;
        private List<string> descriptions;
        public RiddleRepos(GQ dbContext)
        {
            db = dbContext;
            r = new Random();
            descriptions = new List<string>();
            descriptions.Add("Ну-ка, попробуй отгадай!");
            descriptions.Add("Куда лежит твой путь, узнаешь тут");
            descriptions.Add("Молодец, ты почти у цели!");
            descriptions.Add("Вперед, путник, найди сокровище!");
            descriptions.Add("Подарок ждет тебя, забери его");
            db.Riddle.Include("Level_of_complexity").Include("Image").Include("Type_of_question").Include("User").Include("Answer").Load();
        }

        public void Create(Riddle item)
        {
            
            if (item.Description == "") item.Description=GenerDescription();
            db.Riddle.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Riddle k = db.Riddle.Find(id);
            if (k != null) db.Riddle.Remove(k);
            db.SaveChanges();
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
            if (item.Description == "") item.Description = GenerDescription();
            Riddle r = db.Riddle.Find(item.Id_riddle);
            db.Entry(r).CurrentValues.SetValues(item);
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();

        }

        public ObservableCollection<Riddle> GetListForQuest(int id)
        {
            Quest q = db.Quest.Find(id);
            ObservableCollection<Riddle> rid = new ObservableCollection<Riddle>();
            foreach (var t in q.Riddle) rid.Add(db.Riddle.Find(t.Id_riddle));
            return rid;
        }
        public string GenerDescription()
        {
            
            return descriptions[r.Next(0, descriptions.Count)];

        }

        public void Save(bool access_level, bool isNewChecked, Riddle r)
        {
            if (access_level && !isNewChecked)  Update(r);
            else Create(r);
        }
    }
}

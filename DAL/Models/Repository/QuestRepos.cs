﻿using DAL;
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
            db.SaveChanges();
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

          /*  db.SaveChanges();
            //db.Entry(item).State = EntityState.Modified;
            var local = db.Set<Quest>()
                    .Local
                    .FirstOrDefault(f => f.Id_quest == item.Id_quest);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }
            var rs = item.Riddle;
            foreach(var r in rs) db.Entry(r).State = EntityState.Detached;*/
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public ObservableCollection<Quest> GetQuestsForYear(string year)
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@year", year);
            var result = db.Database.SqlQuery<Quest>($"select * from Quest where year(date)=@year", param).ToList();
            var list = new ObservableCollection<Quest>();
            foreach (var i in result) list.Add(db.Quest.Find(i.Id_quest));
            return list;
        }
    }
}

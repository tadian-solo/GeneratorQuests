using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.Models.Repository
{
    public class AnswerRepos
    {
        private GQ db;
        public AnswerRepos(GQ dbContext)
        {
            db = dbContext;
            db.Answer.Load();
        }

        public void Create(Answer item)
        {
            db.Answer.Add(item);
        }

        public void Delete(int id)
        {
            Answer k = db.Answer.Find(id);
            if (k != null) db.Answer.Remove(k);
        }

        public Answer GetItem(int id)
        {
            return db.Answer.Find(id);
        }

        public ObservableCollection<Answer> GetList()
        {
            return db.Answer.Local;
        }

        public void Update(Answer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

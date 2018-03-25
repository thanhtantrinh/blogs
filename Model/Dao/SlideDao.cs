using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using System.Threading.Tasks;

namespace Model.Dao
{
   public  class SlideDao
    {
        OnlineShopEntities db = null;
        public SlideDao()
        {
            db = new OnlineShopEntities();
        }

        public Slide FindSlideId(int id)
        {
            return db.Slides.FirstOrDefault(f=>f.ID==id);
        }
        public long Insert(Slide slide)
        {            
            db.Slides.Add(slide);
            db.SaveChanges();
            return slide.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status.Value == true).OrderBy(y => y.DisplayOrder).ToList();
        }

        public bool SetActive(int id)
        {
            var Slide = FindSlideId(id);
            Slide.Status = true;
            db.SaveChanges();
            return true;
        }
    }
}

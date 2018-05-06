using Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;

namespace Model.Dao
{
    public class MenuDao
    {
        Entities db = null;
        public MenuDao()
        {
            db = new Entities();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }

        public Menu GetByID(int id)
        {
            return db.Menus.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool Update(Menu item)
        {
            try
            {
                var menu = db.Menus.Find(item.ID);
                menu.Link = item.Link;
                menu.Target = item.Target;
                menu.Text = item.Text;
                menu.Status = item.Status;
                menu.TypeID = item.TypeID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
            
        }
        public long Create(Menu item)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(item.Link))
            {
                item.Link = StringHelper.ToUnsignString(item.Text);
            }

            if (string.IsNullOrEmpty(item.Target))
            {
                item.Target = "_self";
            }

            db.Menus.Add(item);
            db.SaveChanges();

            return item.ID;
        }

        public List<MenuView> GetMenuProducts(int CatalogueId=0)
        {
            return db.ProductCategories.Where(w => w.Status == nameof(StatusEntity.Active) && w.CatalogueId== CatalogueId)
                .AsEnumerable()
                .Select(s => new MenuView {
                    ID =s.ID,
                    Link =s.MetaTitle+"-"+s.ID,
                    Target ="",
                    Title =s.Name,
                    Alias=s.MetaTitle
                }).ToList();

        }
    }
}

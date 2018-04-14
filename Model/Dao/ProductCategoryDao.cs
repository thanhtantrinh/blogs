using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using Common;
using AutoMapper;
using PagedList;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopEntities db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopEntities();
        }
        public List<ProductCategory> ListAll()
        {
            var result = db.ProductCategories.OrderBy(x => x.DisplayOrder).ToList();
            return result;
        }
        public IEnumerable<ProductCategoryView> ListAllPaging(string searchString = null, int pageIndex = 1, int pageSize = 10, long catid = 0 , bool isHome = false, bool showAll =  false)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProductCategory, ProductCategoryView>());
            var model = db.ProductCategories;            
            if (!string.IsNullOrEmpty(searchString))
            {                
                model.Where(w => w.Name == searchString.ToLower());
            }
            if (isHome)
            {
                model.Where(w => w.ShowOnHome == true);
            }

            if (catid>0)
            {
                model.Where(w=>w.ID==catid);
            }   
            var result = model.AsEnumerable().Select(x => Mapper.Map<ProductCategory, ProductCategoryView>(x));                      

            result = result.OrderByDescending(x => x.CreatedDate);

            if (showAll)
            {
                return result;
            }

            return result.ToPagedList(pageIndex, pageSize);
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public ProductCategoryView Find(long id)
        {
            var result = new ProductCategoryView();



            ProductCategory productCategory = db.ProductCategories
                .Where(f => f.ID == id)
                .FirstOrDefault();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductCategory, ProductCategoryView>();
            });
            IMapper mapper = config.CreateMapper();

            result= mapper.Map<ProductCategory, ProductCategoryView>(productCategory);

            return result;
        }        
        public long Insert(ProductCategory model)
        {
            if (string.IsNullOrEmpty(model.MetaTitle))
            {
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
            }
            
            model.CreatedBy = 2;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = 2;
            model.ModifiedDate = DateTime.Now;
            
            db.ProductCategories.Add(model);
            db.SaveChanges();
            return model.ID;
        }
        public bool Update(ProductCategory item)
        {
            var category = db.ProductCategories.Find(item.ID);
            category.MetaTitle = item.MetaTitle;
            category.Name = item.Name;
            category.ParentID = item.ParentID;
            category.SeoTitle = item.SeoTitle;
            category.ShowOnHome = item.ShowOnHome;
            category.Status = item.Status;
            category.DisplayOrder = item.DisplayOrder;
            category.MetaDescriptions = item.MetaDescriptions;
            category.MetaKeywords = item.MetaKeywords;
            category.ModifiedDate = DateTime.Now;
            if (db.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }
        public int Delete(int id)
        {
            var item = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(item);
            return db.SaveChanges();
        }
    }
}

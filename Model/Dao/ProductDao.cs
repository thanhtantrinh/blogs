using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using System.Configuration;
using Dapper;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopEntities db = null;        
        public ProductDao()
        {
            db = new OnlineShopEntities();
        }        
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public IEnumerable<ProductsView> ProductsPaging(string searchString="", int pageIndex=1, int pageSize=10, long catid=0, bool isShowHome = false, bool isNew = false)
        {
            var model = db.Products.Join(db.ProductCategories, p => p.CategoryID, pc => pc.ID, (p, pc) => new { ProductCategory = pc, Product = p });
            //search
            if (!String.IsNullOrEmpty(searchString)&& searchString!="")
            {
                model = model.Where(w => w.Product.Name.Contains(searchString));
            }
            
            //filter caterogy
            if (catid > 0)
            {
               model = model.Where(w => w.Product.CategoryID == catid);
            }

            if (isShowHome)
            {
                model = model.Where(w => w.Product.ShowHome == true);
            }


            if (isNew)
            {
                model = model.Where(w => w.Product.New == true);
            }

            var result = model.AsEnumerable().Select(s => new ProductsView
            {
                ID = s.Product.ID,
                Name = s.Product.Name,
                Price = s.Product.Price,
                CatMetaTitle = s.ProductCategory.MetaTitle,
                CatName = s.ProductCategory.Name,
                CreatedDate = s.Product.CreatedDate,               
                MetaTitle = s.Product.MetaTitle,
                Status = s.Product.Status.Value,
                Code=s.Product.Code,
                Description=s.Product.Description,
                IncludedVAT=s.Product.IncludedVAT,
                CategoryID=s.Product.CategoryID,
                CreatedBy=s.Product.CreatedBy,
                Detail=s.Product.Detail,
                IsDiscount=s.Product.PromotionPrice>0?true:false,
                Quantity=s.Product.Quantity,
                MetaDescriptions=s.Product.MetaDescriptions,
                PromotionPrice=s.Product.PromotionPrice,
                MetaKeywords=s.Product.MetaKeywords,
                MoreImages=s.Product.Image,                
                TopHot=s.Product.TopHot,
                ViewCount=s.Product.ViewCount,
                Warranty=s.Product.Warranty
            }).OrderByDescending(o => o.CreatedDate).ToPagedList(pageIndex, pageSize);

            return result;
        }
        public List<Product> ListProduct()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price.Value
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price.Value
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        /// <summary>
        /// List feature product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public ProductsView Find(long id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductsView>());
            var model = db.Products.Where(w => w.ID == id).FirstOrDefault();
            var result = Mapper.Map<Product, ProductsView>(model);
            return result;

        }
        public bool Update(ProductsView product)
        {
            var item = db.Products.Find(product.ID);
            item.Name = product.Name.Trim();
            if (!String.IsNullOrWhiteSpace(product.MetaTitle))
            {
                item.MetaTitle = StringHelper.ToUnsignString(product.MetaTitle.Trim().ToLower());
            }
            item.Price = product.Price;
            item.PromotionPrice = product.PromotionPrice;
            item.Quantity = product.Quantity;
            item.Detail = product.Detail;
            item.Description = product.Description;
            item.Warranty = product.Warranty;
            item.Status = product.Status;
            item.MetaDescriptions = product.MetaDescriptions;
            item.MetaKeywords = product.MetaKeywords;            
            item.ModifiedBy = 2;
            item.ModifiedDate = DateTime.Now;
            item.CategoryID = product.CategoryID;
            item.Code = product.CategoryID.ToString() + "-" + item.ID.ToString();
            item.IncludedVAT = product.IncludedVAT;

            if (!String.IsNullOrWhiteSpace(product.MoreImages))
            {
                item.MoreImages = product.MoreImages;
            }           

            if (product.Images!=null && !String.IsNullOrWhiteSpace(product.Images.FileName))
            {
                item.Image = product.ID.ToString() + "_" + product.Images.FileName;
            }

            item.ShowHome = product.ShowHome;
            if (db.SaveChanges() > 0) return true;
            return false;
        }        
        public Product Add(ProductsView product)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProductsView,Product>());
            var result = Mapper.Map<ProductsView,Product>(product);
            result.CreatedBy = 2;
            result.CreatedDate = DateTime.Now;
            result.ModifiedBy = 2;
            result.ModifiedDate = DateTime.Now;
            result.ViewCount = 0;            
            db.Products.Add(result);
            db.SaveChanges();
            return result;            
        }
        public int Delete(int id)
        {
            var item = db.Products.Find(id);
            db.Products.Remove(item);
            return db.SaveChanges();
        }    

    }
}

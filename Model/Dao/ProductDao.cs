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
        Entities db = null;
        public ProductDao()
        {
            db = new Entities();
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public IEnumerable<v_Product> ProductsPaging(string searchString = "", int pageIndex = 1, int pageSize = 10, long catid = 0, bool isShowHome = false, bool isNew = false)
        {
            var model = db.v_Product.AsQueryable();
            //search
            if (!String.IsNullOrEmpty(searchString) && searchString != "")
            {
                model = model.Where(w => w.ProductName == searchString);
            }

            //filter caterogy
            if (catid > 0)
            {
                model = model.Where(w => w.CategoryId == catid);
            }

            if (isShowHome)
            {
                model = model.Where(w => w.ShowHome == true);
            }


            if (isNew)
            {
                model = model.Where(w => w.New == true);
            }

            return model.OrderByDescending(o => o.CreatedDate).ToPagedList(pageIndex, pageSize); ;
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
            totalRecord = db.Products.Where(x => x.CategoryId == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryId equals b.ID
                         where a.CategoryId == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.Id,
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
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryId equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.Id,
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
                             Price = x.Price
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
            return db.Products.Where(x => x.Id != productId && x.CategoryId == product.CategoryId).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public ProductsView Find(long Id, long proDetailId = 0)
        {
            var model = db.v_Product.Where(w => w.ProductId == Id);
            if (proDetailId > 0)
            {
                model = model.Where(w => w.ProductDetailId == proDetailId);
            }
            var result = Helper.ConvertProductToProductView(model.FirstOrDefault());
            var productDetails = db.v_ProductDetail.Where(w => w.ProductId == result.ID).ToList();

            if (productDetails.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<v_ProductDetail, ProductDetailModel>()
                    .ForMember(v => v.ProductDetailId, co => co.MapFrom(src => src.ProductDetailId))
                    .ForMember(v => v.ProductPrice, co => co.MapFrom(src => src.ProductPrice))
                    .ForMember(v => v.ProductSize, co => co.MapFrom(src => src.ProductSize))
                    .ForMember(v => v.ProductWeight, co => co.MapFrom(src => src.ProductWeight))
                    .ForMember(v => v.PriceTypeName, co => co.MapFrom(src => src.PriceTypeName))
                    .ForMember(v => v.PriceTypeId, co => co.MapFrom(src => src.PriceTypeId))
                    ;
                });
                IMapper mapper = config.CreateMapper();
                result.ProductDetail = mapper.Map<List<v_ProductDetail>, List<ProductDetailModel>>(productDetails);
            }
            else
            {
                result.ProductDetail = new List<ProductDetailModel>() { new ProductDetailModel() };
            }
            return result;
        }

        public bool Update(ProductsView product)
        {
            bool result = true;
            try
            {
                var item = db.Products.Find(product.ID);

                if (item != null)
                {
                    item.Name = product.Name.Trim();
                    if (!String.IsNullOrWhiteSpace(product.MetaTitle))
                    {
                        item.MetaTitle = StringHelper.ToUnsignString(product.MetaTitle.Trim().ToLower());
                    }

                    item.Price = product.ProductPrice;
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
                    item.CategoryId = product.CategoryID;
                    item.Code = product.CategoryID.ToString() + "-" + item.Id.ToString();
                    item.IncludedVAT = product.IncludedVAT;

                    if (!String.IsNullOrWhiteSpace(product.MoreImages))
                    {
                        item.MoreImages = product.MoreImages;
                    }

                    if (product.Images != null && !String.IsNullOrWhiteSpace(product.Images.FileName))
                    {
                        item.Image = product.ID.ToString() + "_" + product.Images.FileName;
                    }
                    item.ShowHome = product.ShowHome;
                    db.SaveChanges();

                    //save product detail info
                    if (product.ProductDetail.Count > 0)
                    {
                        foreach (var prodetail in product.ProductDetail)
                        {
                            if (prodetail.ProductDetailId > 0)
                            {
                                var productDetail = db.ProductDetails.Find(prodetail.ProductDetailId);

                                if (productDetail != null)
                                {
                                    productDetail.ProductId = item.Id;
                                    productDetail.Size = prodetail.ProductSize;
                                    productDetail.Weight = prodetail.ProductWeight;
                                    var price = db.ProductPrices.First(f => f.ProdetailId == productDetail.ProDetailId);
                                    if (price == null)
                                    {
                                        price = new ProductPrice();
                                        price.ProdetailId = productDetail.ProDetailId;
                                        price.PriceTypeId = 1;
                                        price.Price = prodetail.ProductPrice;
                                        db.ProductPrices.Add(price);
                                    }
                                    else
                                    {
                                        price.ProdetailId = productDetail.ProDetailId;
                                        price.PriceTypeId = 1;
                                        price.Price = prodetail.ProductPrice;
                                    }
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                ProductDetail prodetailNew = new ProductDetail();
                                prodetailNew.Size = prodetail.ProductSize;
                                prodetailNew.Weight = prodetail.ProductWeight;
                                prodetailNew.ProductId = product.ID;
                                prodetailNew.UnitOfWeight = "gram";
                                db.ProductDetails.Add(prodetailNew);
                                db.SaveChanges();

                                ProductPrice price = new ProductPrice();
                                price.ProdetailId = prodetailNew.ProDetailId;
                                price.PriceTypeId = 1;
                                price.Price = prodetail.ProductPrice;
                                db.ProductPrices.Add(price);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }          

            return result;
          
        }
        public Product Add(ProductsView product)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProductsView, Product>());
            var result = Mapper.Map<ProductsView, Product>(product);
            result.CreatedBy = 2;
            result.CreatedDate = DateTime.Now;
            result.ModifiedBy = 2;
            result.ModifiedDate = DateTime.Now;
            result.ViewCount = 0;
            db.Products.Add(result);
            db.SaveChanges();

            //save product detail info
            if (product.ProductDetail.Count > 0)
            {
                foreach (var prodetail in product.ProductDetail)
                {
                    if (prodetail.ProductDetailId > 0)
                    {
                        var productDetail = db.ProductDetails.Find(prodetail.ProductDetailId);

                        if (productDetail != null)
                        {
                            productDetail.ProductId = result.Id;
                            productDetail.Size = prodetail.ProductSize;
                            productDetail.Weight = prodetail.ProductWeight;
                            var price = db.ProductPrices.First(f => f.ProdetailId == productDetail.ProDetailId);
                            if (price == null)
                            {
                                price = new ProductPrice();
                                price.ProdetailId = productDetail.ProDetailId;
                                price.PriceTypeId = 1;
                                price.Price = prodetail.ProductPrice;
                                db.ProductPrices.Add(price);
                            }
                            else
                            {
                                price.ProdetailId = productDetail.ProDetailId;
                                price.PriceTypeId = 1;
                                price.Price = prodetail.ProductPrice;
                            }
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ProductDetail prodetailNew = new ProductDetail();
                        prodetailNew.Size = prodetail.ProductSize;
                        prodetailNew.Weight = prodetail.ProductWeight;
                        prodetailNew.ProductId = product.ID;
                        db.ProductDetails.Add(prodetailNew);
                        db.SaveChanges();

                        ProductPrice price = new ProductPrice();
                        price.ProdetailId = prodetailNew.ProDetailId;
                        price.PriceTypeId = 1;
                        price.Price = prodetail.ProductPrice;
                        db.ProductPrices.Add(price);
                        db.SaveChanges();
                    }
                }
            }
            return result;
        }
        public int Delete(int id)
        {
            var item = db.Products.Find(id);
            db.Products.Remove(item);
            return db.SaveChanges();
        }

        public ProductDetail GetProductDetail(long productDetailId=0)
        {
            return db.ProductDetails.Where(w => w.ProDetailId == productDetailId).FirstOrDefault();
        }

        public ProductDetail CreateGetProductDetail(long productId=0)
        {
            var productDetail = new ProductDetail();
            try
            {
                if (productId > 0)
                {
                    productDetail.ProductId = productId;
                    productDetail.Size = "";
                    productDetail.Weight = 0;
                    productDetail.UnitOfWeight ="gram";
                    db.ProductDetails.Add(productDetail);
                    db.SaveChanges();

                    var productPrice = new ProductPrice();
                    productPrice.ProdetailId = productDetail.ProDetailId;
                    productPrice.PriceTypeId = 1;
                    productPrice.Price = 0;
                    //productPrice.
                    db.ProductPrices.Add(productPrice);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                
            }

            return productDetail;
        }

        public bool DeleteProductDeteail(long productDetailId = 0)
        {
            var productDetail = db.ProductDetails.FirstOrDefault(w => w.ProDetailId == productDetailId);
            if (productDetail!=null)
            {
                var productPrice = db.ProductPrices.First(f => f.ProdetailId == productDetail.ProDetailId);
                db.ProductPrices.Remove(productPrice);
                db.ProductDetails.Remove(productDetail);
                if(db.SaveChanges()>0)
                    return true;
            }
            return false;
        }

    }
}

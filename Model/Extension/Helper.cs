using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Repository;
using Model.ViewModel;
using Dapper;
using System.Data.SqlClient;
using Common;
using AutoMapper;
using Model.EF;

namespace Model
{
    public class Helper : BaseRepository
    {
        public Helper() : base()
        {

        }

        public static List<MenuItem> GetMenuByCatalogueId(long catalogueId, out string message)
        {
            var result = new List<MenuItem>();
            message = null;
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();
                    string SqlQuery = "SELECT * FROM v_Menu WHERE CatalogueId=@catalogueId";
                    if (catalogueId >= 0)
                    {
                        parameters.Add("@catalogueId", catalogueId);
                        result = db.Query<MenuItem>(SqlQuery, parameters).ToList();
                    }
                    else
                    {
                        message = String.Format(StaticResources.Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, catalogueId);
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetMenuByCatalogueId at Helper at Model.Extension";
                message += subject + StringHelper.Parameters2ErrorString(ex, catalogueId);

            }
            return result;
        }
        /// <summary>
        /// Get SiteMap from database
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<SiteMapItem> GetSiteMap(long catalogueId, out string message)
        {
            var result = new List<SiteMapItem>();
            message = null;
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();
                    string SqlQuery = "SELECT * FROM v_SiteMap WHERE CatalogueId=@catalogueId";
                    if (catalogueId >= 0)
                    {
                        parameters.Add("@catalogueId", catalogueId);
                        result = db.Query<SiteMapItem>(SqlQuery, parameters).ToList();
                    }
                    else
                    {
                        message = String.Format(StaticResources.Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, catalogueId);
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetSiteMap at Helper at Model.Extension";
                message += subject + StringHelper.Parameters2ErrorString(ex, catalogueId);

            }
            return result;
        }

        public static OrderModel ConvertCartModelToOrder(CartModel checkOut)
        {
            OrderModel result = new OrderModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartModel, CheckoutModel>();
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<CartModel, OrderModel>(checkOut);
            return result;
        }
        /// <summary>
        /// Convert Checkout Model to Order Model
        /// </summary>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public static OrderModel ConvertCheckOutModelToOrder(CheckoutModel checkOut)
        {
            OrderModel result = new OrderModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CheckoutModel, OrderModel>()
                .ForMember(o => o.Address, co => co.MapFrom(src => src.ShippingDetail.Address))
                .ForMember(o => o.Email, co => co.MapFrom(src => src.ShippingDetail.Email))
                .ForMember(o => o.FullName, co => co.MapFrom(src => src.ShippingDetail.Fullname))
                .ForMember(o => o.Phone, co => co.MapFrom(src => src.ShippingDetail.Phone))
                .ForMember(o => o.Notes, co => co.MapFrom(src => src.ShippingDetail.Note));
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<CheckoutModel, OrderModel>(checkOut);
            //Manel
            result.Address = checkOut.ShippingDetail.Address;
            result.Email = checkOut.ShippingDetail.Email;
            result.FullName = checkOut.ShippingDetail.Fullname;
            result.Phone = checkOut.ShippingDetail.Phone;
            result.Notes = checkOut.ShippingDetail.Note;
            result.Items = ConvertCartItemsToOrderItems(checkOut.CartItems);
            return result;
        }
        /// <summary>
        /// Convert the CartItems to OrderItems
        /// </summary>
        /// <param name="CartItems"></param>
        /// <returns></returns>
        public static List<OrderItem> ConvertCartItemsToOrderItems(List<CartItem> CartItems)
        {
            List<OrderItem> result = new List<OrderItem>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartItem, OrderItem>()
                .ForMember(o => o.ProductId, co => co.MapFrom(src => src.ProductId))
                .ForMember(o => o.ProductName, co => co.MapFrom(src => src.ProductName))
                .ForMember(o => o.ProductImage, co => co.MapFrom(src => src.ProductImage))
                .ForMember(o => o.Price, co => co.MapFrom(src => src.Price))
                .ForMember(o => o.Quantity, co => co.MapFrom(src => src.Quantity));
            });
            IMapper mapper = config.CreateMapper();
            foreach (var item in CartItems)
            {
                result.Add(mapper.Map<CartItem, OrderItem>(item));
            }
            return result;
        }
        /// <summary>
        /// Convert a ProductCategoryView To ProductCategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProductCategory ConvertProductCategoryViewToProductCategory(ProductCategoryView model)
        {
            ProductCategory result = new ProductCategory();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductCategoryView, ProductCategory>()
               .ForMember(v => v.ID, co => co.MapFrom(src => src.ID))
               .ForMember(v => v.CatalogueId, co => co.MapFrom(src => src.CatalogueId))
               .ForMember(v => v.ParentID, co => co.MapFrom(src => src.ParentID))
               .ForMember(v => v.MetaTitle, co => co.MapFrom(src => src.MetaTitle))
               .ForMember(v => v.Name, co => co.MapFrom(src => src.Name))
               .ForMember(v => v.DisplayOrder, co => co.MapFrom(src => src.DisplayOrder))
               .ForMember(v => v.SeoTitle, co => co.MapFrom(src => src.SeoTitle))
               .ForMember(v => v.CreatedDate, co => co.MapFrom(src => src.CreatedDate))
               .ForMember(v => v.CreatedBy, co => co.MapFrom(src => src.CreatedBy))
               .ForMember(v => v.ModifiedDate, co => co.MapFrom(src => src.ModifiedDate))
               .ForMember(v => v.ModifiedBy, co => co.MapFrom(src => src.ModifiedBy))
               .ForMember(v => v.MetaKeywords, co => co.MapFrom(src => src.MetaKeywords))
               .ForMember(v => v.MetaDescriptions, co => co.MapFrom(src => src.MetaDescriptions))
               .ForMember(v => v.Status, co => co.MapFrom(src => src.Status))
               .ForMember(v => v.ShowOnHome, co => co.MapFrom(src => src.ShowOnHome))
               ;
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<ProductCategoryView, ProductCategory>(model);
            return result;
        }
        public static ProductCategoryView ConvertProductCategoryToProductCategoryView(v_CategoryOfProduct model)
        {
            ProductCategoryView result = new ProductCategoryView();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_CategoryOfProduct, ProductCategoryView>()
               .ForMember(v => v.ID, co => co.MapFrom(src => src.ID))
               .ForMember(v => v.CatalogueId, co => co.MapFrom(src => src.CatalogueId))
               .ForMember(v => v.CatalogueName, co => co.MapFrom(src => src.CategoryName))
               .ForMember(v => v.ParentID, co => co.MapFrom(src => src.ParentId))
               .ForMember(v => v.ParentName, co => co.MapFrom(src => src.ParentName))
               .ForMember(v => v.MetaTitle, co => co.MapFrom(src => src.MetaTitle))
               .ForMember(v => v.Name, co => co.MapFrom(src => src.CategoryName))
               .ForMember(v => v.DisplayOrder, co => co.MapFrom(src => src.Orders))
               .ForMember(v => v.SeoTitle, co => co.MapFrom(src => src.Title))
               .ForMember(v => v.CreatedDate, co => co.MapFrom(src => src.CreatedDate))
               .ForMember(v => v.CreatedBy, co => co.MapFrom(src => src.CreatedBy))
               .ForMember(v => v.ModifiedDate, co => co.MapFrom(src => src.ModifiedDate))
               .ForMember(v => v.ModifiedBy, co => co.MapFrom(src => src.ModifiedBy))
               .ForMember(v => v.MetaKeywords, co => co.MapFrom(src => src.MetaKeywords))
               .ForMember(v => v.MetaDescriptions, co => co.MapFrom(src => src.MetaDescriptions))
               .ForMember(v => v.Status, co => co.MapFrom(src => src.Status))
               .ForMember(v => v.ShowOnHome, co => co.MapFrom(src => src.ShowOnHome))

               ;
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<v_CategoryOfProduct, ProductCategoryView>(model);
            return result;
        }

        public static ProductsView ConvertProductToProductView(v_Product model)
        {
            ProductsView result = new ProductsView();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Product, ProductsView>()
               .ForMember(v => v.ID, co => co.MapFrom(src => src.ProductId))
               .ForMember(v => v.CatalogueId, co => co.MapFrom(src => src.CatalogueId))
               .ForMember(v => v.CatalogueName, co => co.MapFrom(src => src.CatalogueName))
               .ForMember(v => v.CategoryID, co => co.MapFrom(src => src.CategoryId))
               .ForMember(v => v.CategoryAlias, co => co.MapFrom(src => src.CategoryAlias))
               .ForMember(v => v.MetaTitle, co => co.MapFrom(src => src.ProductAlias))
               .ForMember(v => v.Name, co => co.MapFrom(src => src.ProductName))
               .ForMember(v => v.Code, co => co.MapFrom(src => src.Code))

               .ForMember(v => v.Price, co => co.MapFrom(src => src.Price))
               .ForMember(v => v.PromotionPrice, co => co.MapFrom(src => src.PromotionPrice))
               .ForMember(v => v.Quantity, co => co.MapFrom(src => src.Quantity))
               .ForMember(v => v.Warranty, co => co.MapFrom(src => src.Warranty))
               .ForMember(v => v.TopHot, co => co.MapFrom(src => src.TopHot))
               .ForMember(v => v.ViewCount, co => co.MapFrom(src => src.ViewCount))
               .ForMember(v => v.IsDiscount, co => co.MapFrom(src => src.PromotionPrice > 0 ? true : false))
               .ForMember(v => v.IncludedVAT, co => co.MapFrom(src => src.IncludedVAT))

               .ForMember(v => v.MetaKeywords, co => co.MapFrom(src => src.MetaKeywords))
               .ForMember(v => v.MetaDescriptions, co => co.MapFrom(src => src.MetaDescriptions))
               .ForMember(v => v.Status, co => co.MapFrom(src => src.Status))

               .ForMember(v => v.CreatedDate, co => co.MapFrom(src => src.CreatedDate))
               .ForMember(v => v.CreatedBy, co => co.MapFrom(src => src.CreatedBy))
               .ForMember(v => v.CreatedByName, co => co.MapFrom(src => src.CreatedByName))
               .ForMember(v => v.ModifiedDate, co => co.MapFrom(src => src.ModifiedDate))
               .ForMember(v => v.ModifiedBy, co => co.MapFrom(src => src.ModifiedBy))
               .ForMember(v => v.ModifiedByName, co => co.MapFrom(src => src.ModifiedByName))
               ;
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<v_Product, ProductsView>(model);
            return result;
        }

        public static Product ConvertProductViewToProduct(ProductsView model)
        {
            Product result = new Product();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductsView, Product>()
               .ForMember(v => v.Id, co => co.MapFrom(src => src.ID))
                              .ForMember(v => v.Name, co => co.MapFrom(src => src.Name))
               .ForMember(v => v.Code, co => co.MapFrom(src => src.Code))
               .ForMember(v => v.CatalogueId, co => co.MapFrom(src => src.CatalogueId))
               .ForMember(v => v.CategoryId, co => co.MapFrom(src => src.CategoryID))
               .ForMember(v => v.MetaTitle, co => co.MapFrom(src => src.MetaTitle))
               .ForMember(v => v.Description, co => co.MapFrom(src => src.Description))
               .ForMember(v => v.Detail, co => co.MapFrom(src => src.Detail))
               .ForMember(v => v.Price, co => co.MapFrom(src => src.Price))
               .ForMember(v => v.PromotionPrice, co => co.MapFrom(src => src.PromotionPrice))
               .ForMember(v => v.IncludedVAT, co => co.MapFrom(src => src.IncludedVAT))
               .ForMember(v => v.Quantity, co => co.MapFrom(src => src.Quantity))
               .ForMember(v => v.Warranty, co => co.MapFrom(src => src.Warranty))
               .ForMember(v => v.TopHot, co => co.MapFrom(src => src.TopHot))
               .ForMember(v => v.ViewCount, co => co.MapFrom(src => src.ViewCount))               
               .ForMember(v => v.MetaKeywords, co => co.MapFrom(src => src.MetaKeywords))
               .ForMember(v => v.MetaDescriptions, co => co.MapFrom(src => src.MetaDescriptions))
               .ForMember(v => v.Status, co => co.MapFrom(src => src.Status))               
               .ForMember(v => v.ShowHome, co => co.MapFrom(src => src.ShowHome))
               .ForMember(v => v.CreatedDate, co => co.MapFrom(src => src.CreatedDate))
               .ForMember(v => v.CreatedBy, co => co.MapFrom(src => src.CreatedBy))
               .ForMember(v => v.ModifiedDate, co => co.MapFrom(src => src.ModifiedDate))
               .ForMember(v => v.ModifiedBy, co => co.MapFrom(src => src.ModifiedBy))
               ;
            });
            IMapper mapper = config.CreateMapper();
            result = mapper.Map<ProductsView, Product>(model);
            return result;
        }
    }
}

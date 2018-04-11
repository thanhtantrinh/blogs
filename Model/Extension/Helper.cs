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
    }
}

using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Common;
using Model.EF;
using AutoMapper;

namespace Model.Repository
{
    public class CatalogueRepo: BaseRepository
    {
        public CatalogueRepo() : base(conn)
        {

        }

        public CatalogueRepo(string DbConnectionString)
        {
            conn = DbConnectionString;
        }

        public CatalogueView GetCatalogue(int catalogueId, out string message)
        {
            CatalogueView result = new CatalogueView();
            message = null;
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    var parameters = new DynamicParameters();
                    string SqlQuery = "SELECT top 1 * FROM v_CatalogueInfo WHERE Id=@catalogueId";
                    if (catalogueId > 0)
                    {
                        parameters.Add("@catalogueId", catalogueId);
                        result = db.Query<CatalogueView>(SqlQuery, parameters).FirstOrDefault();
                    }
                    else
                    {
                        message = String.Format(StaticResources.Resources.SYSTEM_ERROR_THE_PARAMETER_LARGE_ZERO, catalogueId);
                    }                    
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetCatalogue at CatalogueRepo at Model.Repository";
                message += subject + StringHelper.Parameters2ErrorString(ex, catalogueId);
            }
            return result;
        }

        public CatalogueView UpdateCatalogue(CatalogueView model, out string message)
        {
            CatalogueView result = new CatalogueView();
            message = null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<v_CatalogueInfo, CatalogueView>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                using (var entities = new OnlineShopEntities())
                {
                    var catalogue = entities.Catalogues.SingleOrDefault(s=>s.Id==model.Id);
                    ///site info
                    catalogue.SiteName = model.SiteName;
                    catalogue.SiteUrl = model.SiteUrl;
                    ///email
                    catalogue.EmailSite = model.EmailSite;                    
                    catalogue.EmailAdmin = model.EmailAdmin;
                    ///address info
                    catalogue.Address = model.Address;
                    catalogue.Phones = model.Phones;
                    ///seo
                    catalogue.MetaKeywords = model.MetaKeywords;
                    catalogue.MetaDescriptions = model.MetaDescriptions;
                    catalogue.Facebook = model.Facebook;
                    catalogue.Twitter = model.Twitter;
                    catalogue.Youtube = model.Youtube;
                    //system
                    catalogue.ModifiedDate = DateTime.Now;
                    catalogue.ModifiedBy = model.ModifiedById;

                    if (entities.SaveChanges() > 0)
                    {
                        v_CatalogueInfo catalogueInfo = entities.v_CatalogueInfo.Where(w => w.Id == catalogue.Id).FirstOrDefault();                        
                        result = mapper.Map<v_CatalogueInfo, CatalogueView>(catalogueInfo);
                    }
                    else
                    {
                        message = StaticResources.Resources.SYSTEM_ERROR_THE_UPDATING_WEBSITE_CONFI_HAS_BEEN_FINISHED;
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at UpdateCatalogue at CatalogueRepo at Model.Repository. ";
                message += subject + StringHelper.Parameters2ErrorString(ex, model.Id);
            }

            return result;
        }


    }
}

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
using PagedList;

namespace Model.Repository
{
    public class CatalogueRepo : BaseRepository
    {
        public CatalogueRepo() : base()
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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_CatalogueInfo, CatalogueView>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                using (var entities = new OnlineShopEntities())
                {
                    var catalogue = entities.Catalogues.SingleOrDefault(s => s.Id == model.Id);
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

        public IEnumerable<v_CatalogueInfo> GetCataloguesPaging(CatalogueFilter filter, int pageNumber = 1, int pageSize = 20, string SortBy = "")
        {
            IQueryable<v_CatalogueInfo> model = entities.v_CatalogueInfo;
            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.CatalogueName.Contains(searchString));
                }

                if (!String.IsNullOrWhiteSpace(filter.Status))
                {
                    switch (filter.Status.Trim())
                    {
                        case nameof(StatusEntity.Active):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Active));
                            break;
                        case nameof(StatusEntity.Locked):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Locked));
                            break;
                        case nameof(StatusEntity.Deleted):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Deleted));
                            break;
                        default:
                            model = model.Where(w => w.Status != nameof(StatusEntity.Deleted));
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(SortBy))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetCataloguesPaging at CatalogueRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }

            return model.ToPagedList(pageNumber, pageSize);
        }
        /// <summary>
        /// Tạo mới catalogue
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CatalogueView CreateCatalogue(CatalogueView model, out string message)
        {
            CatalogueView result = new CatalogueView();
            message = null;
            Catalogue catalogue = new Catalogue();
            try
            {
                if (model != null)
                {
                    ///site info
                    ///
                    catalogue.SiteName = model.SiteName;
                    catalogue.SiteUrl = model.SiteUrl;
                    catalogue.Name = model.CatalogueName;
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
                    catalogue.CreatedBy = model.CreatedById;
                    catalogue.CreatedDate = DateTime.Now;
                    catalogue.ModifiedDate = DateTime.Now;
                    catalogue.ModifiedBy = model.ModifiedById;
                    catalogue.Status = model.Status;

                    catalogue = entities.Catalogues.Add(catalogue);
                    if (entities.SaveChanges() > 0)
                    {
                        result = this.GetCatalogue(catalogue.Id, out message);
                    }
                    else
                    {
                        message = StaticResources.Resources.SYSTEM_ERROR_THE_UPDATING_WEBSITE_CONFI_HAS_BEEN_FINISHED;
                    }
                }
                else
                {
                    message = StaticResources.Resources.MSG_THE_CATEGORY_HAS_CREATED_UNSUCCESSFULLY;
                }

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at CreateCatalogue at CatalogueRepo at Model.Repository. ";
                message += subject + StringHelper.Parameters2ErrorString(ex, model.Id);
            }

            return result;

        }
    }
}

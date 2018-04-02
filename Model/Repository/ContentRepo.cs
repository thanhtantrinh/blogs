using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PagedList;
using Common;
using System.Text.RegularExpressions;

namespace Model.Repository
{
    public class ContentRepo : BaseRepository
    {

        public ContentRepo() : base()
        {

        }
        /// <summary>
        /// get list view content và paging sort filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortby"></param>
        /// <returns></returns>
        public IEnumerable<v_Content> GetContentPaging(ContentFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            IQueryable<v_Content> model = entities.v_Content;

            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.Name.Contains(searchString));
                }

                if (filter.CatalogueId > 0)
                    model = model.Where(x => x.CatalogueId == filter.CatalogueId);

                if (filter.CategoryID > 0)
                {
                    model = model.Where(x => x.CategoryID == filter.CategoryID);
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

                if (!String.IsNullOrWhiteSpace(sortby))
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
                string subject = "Error " + SiteSetting.SiteName + " at GetContentPaging at ContentRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model.ToPagedList(pageIndex, pageSize);

        }

        /// <summary>
        /// get content by Id và trả về ContentViewModel
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ContentViewModel GetContentById(long Id = 0)
        {
            var model = new ContentViewModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Content, ContentViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                if (Id > 0)
                {
                    var result = entities.v_Content.FirstOrDefault(w => w.ID == Id);
                    model = mapper.Map<ContentViewModel>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at getContentById at ContentRepo at Model.Repository ";
                string message = StringHelper.Parameters2ErrorString(ex, Id);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model;
        }

        public ContentViewModel Create(ContentViewModel model, out string message)
        {
            ContentViewModel result = new ContentViewModel();
            message = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Content, ContentViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                Content content = new Content();
                //Xử lý alias
                //
                content.CategoryID = model.CategoryID;
                content.Name = model.Name.Trim();
                content.Description = Regex.Replace(model.Description.Trim(), @"<[^>]*>", String.Empty);
                content.Detail = model.Detail;
                content.Image = model.Image;

                //for SEO
                if (string.IsNullOrEmpty(model.MetaTitle))
                    content.MetaTitle = StringHelper.ToUnsignString(model.Name);
                else
                    content.MetaTitle = model.MetaTitle;

                content.MetaDescriptions = model.MetaDescriptions;
                content.MetaKeywords = model.MetaKeywords;

                content.CreatedDate = DateTime.Now;
                content.CreatedBy = model.CreatedBy;
                content.ModifiedDate = DateTime.Now;
                content.ModifiedBy = model.ModifiedBy;

                content.Status = model.Status;
                content.Language = model.Language;
                content.ViewCount = 0;
                

                entities.Contents.Add(content);
                if (entities.SaveChanges() > 0)
                {
                    result = mapper.Map<ContentViewModel>(GetContentById(content.ID));
                }
                else
                {
                    message = StaticResources.Resources.MSG_THE_CONTENT_HAS_CREATED_UNSUCCESSFULLY;
                }

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Create at ContentDao at Model.Dao. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }
        public ContentViewModel Update(ContentViewModel model, out string message)
        {
            ContentViewModel result = new ContentViewModel();
            message = null;
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<v_Content, ContentViewModel>();
            //});
            //IMapper mapper = config.CreateMapper();
            try
            {
                var content = entities.Contents.Find(model.ID);

                if (content != null)
                {
                    content.CategoryID = model.CategoryID;
                    content.CatalogueId = model.CatalogueId;
                    content.Name = model.Name.Trim();
                    content.Description = Regex.Replace(model.Description.Trim(), @"<[^>]*>", String.Empty);
                    content.Detail = model.Detail;
                    content.Image = model.Image;

                    //for SEO
                    content.MetaTitle = model.MetaTitle;
                    content.MetaDescriptions = model.MetaDescriptions;
                    content.MetaKeywords = model.MetaKeywords;

                    content.ModifiedDate = DateTime.Now;
                    content.ModifiedBy = model.ModifiedBy;
                    content.Status = model.Status;
                    content.Language = model.Language;

                    if (entities.SaveChanges() > 0)
                    {
                        result = GetContentById(content.ID);
                    }
                    else
                    {
                        message = StaticResources.Resources.MSG_THE_CONTENT_HAS_UPDATED_UNSUCCESSFULLY;
                    }
                }
                else
                {
                    message = StaticResources.Resources.MSG_THE_CONTENT_HAS_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Update at ContentDao at Model.Dao. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;

        }


    }
}

using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using PagedList;
using System.Web;
using System.Text.RegularExpressions;
using Model.ViewModel;
using AutoMapper;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopEntities db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ContentDao()
        {
            db = new OnlineShopEntities();
        }
        public IEnumerable<v_Content> ListAllPaging(ContentFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            var model = db.v_Content.AsEnumerable();

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                string searchString = filter.SearchString.Trim();
                model = model.Where(x => x.Name.Contains(searchString));
            }

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
                    default:
                        model = model.Where(w => w.Status != nameof(StatusEntity.Deleted));
                        break;
                }
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(pageIndex, pageSize);

        }
        public List<Content> ListNewsEvent(long catid)
        {
            var model = db.Contents.Where(w => w.CategoryID == catid).ToList();
            return model;
        }
        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public IEnumerable<Content> ListContentByCatId(long catid)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).Where(x => x.CategoryID == catid);
        }
        public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public ContentViewModel GetByID(long Id = 0)
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
                    using (db)
                    {
                        var result = db.v_Content.FirstOrDefault(w => w.ID == Id);
                        model = mapper.Map<ContentViewModel>(result);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetByID at ContentDao at Model.Model";
                string message = StringHelper.Parameters2ErrorString(ex, Id);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model;
        }
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
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


                using (var entity = db)
                {
                    entity.Contents.Add(content);
                    if (db.SaveChanges() > 0)
                    {
                        result = mapper.Map<ContentViewModel>(db.v_Content.FirstOrDefault(m => m.ID == content.ID));
                    }
                    else
                    {
                        message = StaticResources.Resources.MSG_THE_CONTENT_HAS_CREATED_UNSUCCESSFULLY;
                    }
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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Content, ContentViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            try
            {
                var content = db.Contents.Find(model.ID);

                if (content != null)
                {
                    content.CategoryID = model.CategoryID;
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

                    if (db.SaveChanges() > 0)
                    {
                        result = mapper.Map<ContentViewModel>(db.v_Content.FirstOrDefault(m => m.ID == model.ID));
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
        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string tagstring)
        {
            return db.Tags.Count(x => x.Name == tagstring) > 0;
        }

    }
}

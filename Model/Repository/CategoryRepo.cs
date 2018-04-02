using Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Model.EF;
using PagedList;
using AutoMapper;

namespace Model.Repository
{
    public class CategoryRepo : BaseRepository
    {

        public CategoryRepo() : base()
        {

        }

        /// <summary>
        /// get catagory, paging, sort, filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortby"></param>
        /// <returns></returns>
        public IEnumerable<v_Category> GetCategoriesPaging(CategoryFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            IQueryable<v_Category> model = entities.v_Category;
            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.Name.Contains(searchString));
                }

                if (filter.CatalogueId > 0)
                    model = model.Where(x => x.CatalogueId == filter.CatalogueId);

                if (filter.ParentID > 0)
                {
                    model = model.Where(x => x.CatalogueId == filter.ParentID);
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
                string subject = "Error " + SiteSetting.SiteName + " at GetCategoriesPaging at CategoryRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model.ToPagedList(pageIndex, pageSize);
        }
        /// <summary>
        /// Get catagory by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CategoryView GetCategoryById(long Id = 0)
        {
            var model = new CategoryView();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Category, CategoryView>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                if (Id > 0)
                {
                    var result = entities.v_Category.Where(w => w.ID == Id).FirstOrDefault();
                    model = mapper.Map<CategoryView>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetCategoryById at CategoryRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, Id);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model;
        }
        /// <summary>
        /// Edit Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CategoryView Edit(CategoryView model, out string message)
        {
            CategoryView result = new CategoryView();
            message = null;
            try
            {
                var category = entities.Categories.Find(model.ID);
                if (category != null)
                {
                    category.Name = model.Name;
                    category.ParentID = model.ParentID;
                    category.Language = model.Language;
                    category.Image = model.Image;
                    category.ShowOnHome = model.ShowOnHome;
                    category.CatalogueId = model.CatalogueId;
                    

                    if (string.IsNullOrEmpty(model.MetaTitle))
                    {
                        model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                    }

                    category.MetaTitle = model.MetaTitle;
                    category.SeoTitle = model.SeoTitle;
                    category.MetaDescriptions = model.MetaDescriptions;
                    category.MetaKeywords = model.MetaKeywords;

                    category.Status = model.Status;
                    category.ModifiedBy = model.ModifiedBy;
                    category.ModifiedDate = DateTime.Now;

                    if (entities.SaveChanges() > 0 && category.ID > 0)
                    {
                        result = GetCategoryById(category.ID);
                    }
                    else
                    {
                        message = StaticResources.Resources.MSG_THE_CATEGORY_HAS_UPDATED_UNSUCCESSFULLY;
                    }
                }
                else
                {
                    message = StaticResources.Resources.MSG_THE_CATEGORY_HAS_NOT_FOUND;
                }

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Edit at CategoryRepo at Model.Repository ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }

        public CategoryView Create(CategoryView model, out string message)
        {
            CategoryView result = new CategoryView();
            message = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryView, Category>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                if (model != null)
                {
                    Category category = mapper.Map<Category>(model);

                    category.CreatedBy = model.CreatedBy;
                    category.CreatedDate = DateTime.Now;
                    category.ModifiedBy = model.CreatedBy;
                    category.ModifiedDate = DateTime.Now;
                    
                    entities.Categories.Add(category);
                    if (entities.SaveChanges() > 0)
                    {
                        result = GetCategoryById(category.ID);
                    }
                    else
                    {
                        message = StaticResources.Resources.SYSTEM_ERROR_EXECUTE_SAVECHANGE_IN_ENTITY;
                        return null;
                    }

                }
                else
                {
                    message = StaticResources.Resources.SYSTEM_ERROR_MODEL_IS_NULL;
                    return null;
                }

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Create at CategoryRepo at Model.Repository. ";
                message = StringHelper.Parameters2ErrorString(ex, model.Name);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;

        }
    }
}

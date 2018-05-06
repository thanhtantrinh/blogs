using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;
using Model.ViewModel;
using AutoMapper;

namespace Model.Dao
{
    public class CategoryDao
    {
        Entities db = null;
        public CategoryDao()
        {
            db = new Entities();
        }

        public CategoryView GetByID(long Id = 0)
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
                    using (db)
                    {
                        var result = db.v_Category.Where(w => w.ID == Id).FirstOrDefault();
                        model = mapper.Map<CategoryView>(result);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at load at Model.Model";
                string message = StringHelper.Parameters2ErrorString(ex, Id);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model;
        }

        public CategoryView Insert(CategoryView model, out string message)
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
                    using (db)
                    {
                        Category category = mapper.Map<Category>(model);

                        category.CreatedDate = DateTime.Now;
                        category.ModifiedBy = model.CreatedBy;
                        category.ModifiedDate = DateTime.Now;

                        db.Categories.Add(category);
                        if (db.SaveChanges() > 0)
                        {
                            result = GetByID(category.ID);                           
                        }
                        else
                        {
                            message = StaticResources.Resources.SYSTEM_ERROR_EXECUTE_SAVECHANGE_IN_ENTITY;
                            return null;
                        }
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
                string subject = "Error " + SiteSetting.SiteName + " at Insert at CategoryDao at Model.Dao. ";
                message = StringHelper.Parameters2ErrorString(ex, model.Name);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;

        }
        public CategoryView Edit(CategoryView model, out string message)
        {
            CategoryView result = new CategoryView();
            message = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<v_Category, CategoryView>();
            });
            IMapper mapper = config.CreateMapper();
            try
            {
                using (db)
                {
                    var category = db.Categories.Find(model.ID);
                    if (category != null)
                    {
                        category.Name = model.Name;
                        category.ParentID = model.ParentID;
                        //category.Language = model.Language;
                        category.Image = model.Image;
                        category.ShowOnHome = model.ShowOnHome;

                        category.Status = model.Status;

                        if (string.IsNullOrEmpty(model.MetaTitle))
                        {
                            model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                        }
                        category.MetaTitle = model.MetaTitle;
                        category.SeoTitle = model.SeoTitle;
                        category.MetaDescriptions = model.MetaDescriptions;
                        category.MetaKeywords = model.MetaKeywords;
                        category.ModifiedBy = model.ModifiedBy;
                        if (db.SaveChanges() > 0)
                        {
                            result = mapper.Map<CategoryView>(db.v_Category.FirstOrDefault(m => m.ID == model.ID));
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

            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Edit at CategoryDao at Model.Dao. ";
                message = StringHelper.Parameters2ErrorString(ex, model.ID);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }
        public bool Update(Category item)
        {
            try
            {
                var category = db.Categories.Find(item.ID);
                if (string.IsNullOrEmpty(item.MetaTitle))
                {
                    category.MetaTitle = StringHelper.ToUnsignString(item.Name);
                }
                else
                {
                    category.MetaTitle = item.MetaTitle;
                }
                category.Image = item.Image;
                category.Name = item.Name;
                category.ParentID = item.ParentID;
                category.SeoTitle = item.SeoTitle;
                category.ShowOnHome = item.ShowOnHome;
                category.Status = item.Status;
                category.DisplayOrder = item.DisplayOrder;
                category.MetaDescriptions = item.MetaDescriptions;
                category.MetaKeywords = item.MetaKeywords;
                category.ModifiedDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at Edit at Update at Model.Dao. ";
                //message = StringHelper.Parameters2ErrorString(ex, item.ID);
                //MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);

            }
            return true;
        }

        public List<Category> ListAll()
        {
            var model = db.Categories.Where(x => x.Status == nameof(StatusEntity.Active)).ToList();
            return model;
        }

        public IEnumerable<v_Category> ListAllPaging(CategoryFilter filter, int page, int pageSize)
        {
            IQueryable<v_Category> model = db.v_Category;

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                string searchString = filter.SearchString.Trim();
                model = model.Where(x => x.Name.Contains(searchString));
            }

            if (filter.ParentID > 0)
            {
                model = model.Where(w => w.ParentID == filter.ParentID);
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

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public CategoryBlogViewModel CategoryBlog(long Id)
        {
            //var content = db.Contents.Where(w => w.CategoryID == Id).ToList();
            //var Cat = db.Categories.Where(w => w.ID == Id).SingleOrDefault();
            //var CatBlog = new CategoryBlogViewModel();
            //CatBlog.ID = Cat.ID;
            //CatBlog.Name = Cat.Name;
            //CatBlog.Products = content;
            var CatBlog = db.Categories.Where(w => w.ID == Id)
                            .Select(s => new CategoryBlogViewModel()
                            {
                                ID = s.ID,
                                Image = s.Image,
                                Name = s.Name,
                                Products = db.Contents.Where(w => w.CategoryID == Id).Take(5).ToList()
                            }).SingleOrDefault();

            return CatBlog;
        }
        public List<CategoryBlogViewModel> Products(long id)
        {
            var Result = db.Categories.Where(w => w.ParentID == id).OrderBy(o => o.DisplayOrder)
                .Select(s => new CategoryBlogViewModel()
                {
                    ID = s.ID,
                    Image = s.Image,
                    Name = s.Name,
                    Products = db.Contents.Where(w => w.CategoryID == s.ID).ToList()
                }).ToList();
            return Result;
        }

    }
}

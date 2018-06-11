using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Model.EF;
using Common;
using System.Runtime.Caching;
using Model.Repository;
using System.ComponentModel.DataAnnotations;

namespace Model.Extension
{
    public class ListExtensions
    {

        /// <summary>
        /// Get 10 years nearly
        /// </summary>
        public static List<SelectListItem> Years
        {
            get
            {
                var years = new List<SelectListItem>() {
                    new SelectListItem() { Text = "Year", Value = "" }
                };

                for (int i = 0; i < 10; i++)
                {
                    var year = (DateTime.Now.Year + i).ToString();
                    var yy = year.Substring(2);
                    years.Add(new SelectListItem()
                    {
                        Text = year,
                        Value = yy
                    });
                }
                return years;
            }
        }


        /// <summary>
        /// Get Provinces
        /// </summary>
        public static List<SelectListItem> ProvinceList
        {
            get
            {
                ObjectCache cache = MemoryCache.Default;
                string CacheKey = CacheList.CacheKeyProvince;
                var provinceList = new List<SelectListItem>();

                if (cache.Contains(CacheKey))
                {
                    provinceList = (List<SelectListItem>)cache.Get(CacheKey);
                }
                else
                {
                    using (var db = new Entities())
                    {
                        provinceList = db.Provinces.Where(w => w.IsPublished.Value == true).OrderBy(o => o.Type)
                                            .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
                    }
                    // Store data in the cache    
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(4.0);
                    cache.Add(CacheKey, provinceList, cacheItemPolicy);
                }
                return provinceList;
            }
        }

        /// <summary>
        /// Get Categories
        /// </summary>
        public static List<SelectListItem> CategoryList
        {
            get
            {
                var categoryList = new List<SelectListItem>();
                using (var db = new Entities())
                {
                    categoryList = db.Categories.Where(w => w.Status == nameof(StatusEntity.Active))
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
                    categoryList.Insert(0, new SelectListItem { Text = "Nhóm Root", Value = "0" });

                }
                return categoryList;
            }
        }
        /// <summary>
        /// Get all Category 
        /// </summary>
        public static List<SelectListItem> ProductCategoryList
        {
            get
            {
                var categoryList = new List<SelectListItem>();
                using (var db = new Entities())
                {
                    categoryList = db.ProductCategories.Where(w => w.Status == nameof(StatusEntity.Active))
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
                    categoryList.Insert(0, new SelectListItem { Text = "Nhóm Root", Value = "0" });

                }
                return categoryList;
            }
        }
        /// <summary>
        /// Get all Category 
        /// </summary>
        public static List<SelectListItem> ProductCategoriesByCatalogueId(long catalogueId = 0)
        {
            var categoryList = new List<SelectListItem>();
            var proCatRepo = new ProductCategoryRepo();
            var menu = proCatRepo.GetMenuCategoryProduct(catalogueId).ToList();
            var menuTemp = new List<ViewModel.MenuItem>();
            menuTemp = menu.Where(w => w.Level == 0).OrderBy(o=>o.Order).ToList();
            var menuChirld = menu.Where(w => w.Level != 0).OrderByDescending(o => o.Order).ToList();

            foreach (var item in menuChirld)
            {
                var menuParent = menuTemp.Where(w => w.Id == item.ParentId).FirstOrDefault();
                if (menuParent != null)
                {
                    var positon = menuTemp.IndexOf(menuParent);
                    item.Name = "--" + item.Name;
                    if (menuTemp.Count > positon)
                    {
                        menuTemp.Insert(positon + 1, item);
                    }
                    else
                    {
                        menuTemp.Insert(positon, item);
                    }
                }
            }

            categoryList = menuTemp.AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
            categoryList.Insert(0, new SelectListItem { Text = "Nhóm Root", Value = "0" });
            return categoryList;

        }
        /// <summary>
        /// Get all Category 
        /// </summary>
        public static List<SelectListItem> CategoriesByCatalogueId(long catalogueId = 0)
        {

            var items = new List<SelectListItem>();
            using (var db = new Entities())
            {
                var model = db.Categories.Where(w => w.Status == nameof(StatusEntity.Active));

                if (catalogueId > 0)
                {
                    model = model.Where(w => w.CatalogueId == catalogueId);
                }

                items = model.AsEnumerable().Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.ID.ToString()
                })
                .ToList();

                items.Insert(0, new SelectListItem { Text = "Nhóm Root", Value = "0" });
            }
            return items;

        }

        /// <summary>
        /// Get all catalogue
        /// </summary>
        public static List<SelectListItem> CatalogueList
        {
            get
            {
                var catalogueList = new List<SelectListItem>();
                using (var db = new Entities())
                {
                    catalogueList = db.Catalogues.Where(w => w.Status == nameof(StatusEntity.Active) && w.Id != 0)
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() }).ToList();
                    catalogueList.Insert(0, new SelectListItem { Text = "Tất cả", Value = "" });
                }
                return catalogueList;
            }
        }

        /// <summary>
        /// get status of the entity
        /// </summary>
        public static List<SelectListItem> StatusEntityList
        {
            get
            {
                var selectListItems = Enum.GetValues(typeof(StatusEntity)).Cast<StatusEntity>().Select(v => new SelectListItem
                {
                    Text = v.GetAttribute<DisplayAttribute>().Name,
                    Value = v.ToString()
                }).ToList();
                selectListItems.Insert(0, new SelectListItem { Text = "Chọn trạng thái", Value = "" });
                return selectListItems;
            }
        }
        /// <summary>
        /// get status of order
        /// </summary>
        public static List<SelectListItem> StatusOrderList
        {
            get
            {
                var selectListItems = Enum.GetValues(typeof(eOrderStatusUI)).Cast<eOrderStatusUI>().Select(v => new SelectListItem
                {
                    Text = v.GetAttribute<DisplayAttribute>().Name,
                    Value = v.ToString()
                }).ToList();

                selectListItems.Insert(0, new SelectListItem { Text = "Chọn trạng thái", Value = "" });
                return selectListItems;
            }
        }
        public static List<SelectListItem> PriceTypeList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                using (var db = new Entities())
                {
                    items = db.ProductPriceTypes.Where(w => w.PriceTypeId > 0)
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.PriceTypeName, Value = s.PriceTypeId.ToString() }).ToList();
                    //catalogueList.Insert(0, new SelectListItem { Text = "Tất cả", Value = "" });
                }
                return items;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Model.EF;
using Common;

namespace Model.Extension
{
    public class ListExtensions
    {
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
        /// Get all Category 
        /// </summary>
        public static List<SelectListItem> CategoryList
        {
            get
            {
                var categoryList = new List<SelectListItem>();
                using (var db = new OnlineShopEntities())
                {
                    categoryList = db.Categories.Where(w => w.Status == nameof(StatusEntity.Active))
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
                    categoryList.Insert(0, new SelectListItem { Text = "Nhóm Root", Value = "0" });

                }
                return categoryList;
            }
        }
        /// <summary>
        /// Get all catalogue
        /// </summary>
        public static List<SelectListItem> CatalogueList
        {
            get
            {
                var catalogueList = new List<SelectListItem>();
                using (var db = new OnlineShopEntities())
                {
                    catalogueList = db.Catalogues.Where(w => w.Status == nameof(StatusEntity.Active) && w.Id!=0)
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
                //var selectListItems = (from StatusEntity d in Enum.GetValues(typeof(StatusEntity))
                //                      select new SelectListItem() { Value = nameof(d), Text = nameof(d).ToLower() }).ToList();

                var selectListItems = Enum.GetValues(typeof(StatusEntity)).Cast<StatusEntity>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    //Value = ((int)v).ToString()
                    Value = v.ToString()
                }).ToList();

                selectListItems.Insert(0, new SelectListItem { Text = "Chọn trạng thái", Value = "" });
                return selectListItems;      
            }
        }
    }
}

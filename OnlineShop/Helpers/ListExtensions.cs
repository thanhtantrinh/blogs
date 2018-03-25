using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using Model.EF;

namespace OnlineShop.Helpers
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

        public static List<SelectListItem> ProductCategoryList
        {
            get
            {
                using (var db = new OnlineShopEntities())
                {
                    var categoryList = db.ProductCategories.Where(w => w.Status == true)
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
                    categoryList.Insert(0, new SelectListItem { Text = "Chọn nhóm sản phẩm", Value = "0" });
                    //categoryList.Insert(1, new SelectListItem { Text = "Nhóm Root", Value = "0" });
                    return categoryList;
                }
            }
        }
        public static List<SelectListItem> CategoryListSelected(long Id = 0)
        {
            var categoryList = new List<SelectListItem>();

            using (var db = new OnlineShopEntities())
            {
                categoryList = db.Categories.Where(w => w.Status == true)
                                    .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString(), Selected = s.ID == Id ? true : false }).ToList();
                categoryList.Insert(0, new SelectListItem { Text = "Chọn nhóm bài viết", Value = "" });
                categoryList.Insert(1, new SelectListItem { Text = "Nhóm Root", Value = "0" });

            }
            return categoryList;
        }

        public static List<SelectListItem> CategoryList
        {
            get
            {
                var categoryList = new List<SelectListItem>();
                using (var db = new OnlineShopEntities())
                {
                    categoryList = db.Categories.Where(w => w.Status == true)
                                        .AsEnumerable().Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
                    categoryList.Insert(0, new SelectListItem { Text = "Chọn nhóm bài viết", Value = "" });
                    categoryList.Insert(1, new SelectListItem { Text = "Nhóm Root", Value = "0" });

                }
                return categoryList;
            }
        }

        public static List<SelectListItem> StatusList
        {
            get
            {
                return new List<SelectListItem>() {
                    new SelectListItem() { Text = "Tất cả trạng thái", Value = "" },
                    new SelectListItem() { Text = "Hoạt động", Value = "1" },
                    new SelectListItem() { Text = "Không hoạt động", Value = "0" }
                };
            }
        }
    }
}
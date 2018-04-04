using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.Dao;
using Model;
using Blogs.Helpers;

namespace Blogs.Controllers
{
    public class BaseController : Controller
    {
        public ContentDao _contentDAO = new ContentDao();
        public CategoryDao _categoryDAO = new CategoryDao();

        // GET: Base
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = Url.AbsoluteAction("Index", "Home", null, Request.Url.Scheme),
                    Priority = 1,
                    Frequency = SitemapFrequency.Daily,
                });

            nodes.Add(
            new SitemapNode()
            {
                Url = Url.AbsoluteAction("About", "Home", null, Request.Url.Scheme),
                Priority = 0.9
            });

            //add catagory
            string message = "";
            var model = Helper.GetSiteMap(SiteConfiguration.CatalogueId, out message);

            if (String.IsNullOrWhiteSpace(message))
            {
                foreach (var item in model)
                {
                    //@Url.Action("Detail", "Category", new { Id = item.Id, alias = item.Alias })

                    if (item.IsCategory == 1)
                    {
                        nodes.Add(
                            new SitemapNode()
                            {
                                Url = Url.AbsoluteAction("Detail", "Category", new { Id = item.Id, alias = item.Alias }, Request.Url.Scheme),
                                Priority = 0.9,
                                LastModified = item.ModifiedDate,
                                Frequency = SitemapFrequency.Daily
                            }
                        );
                    }
                    else if (item.IsCategory == 0)
                    {
                        nodes.Add(
                            new SitemapNode()
                            {
                                Url = Url.AbsoluteAction("ContentDetail", "Category", new { id = item.Id, alias = item.Alias, CategoryAlias = item.CategoryAlias, CategoryId = item.CategoryID }, Request.Url.Scheme),
                                Priority = 0.9,
                                LastModified = item.ModifiedDate,
                                Frequency = SitemapFrequency.Weekly
                            }
                        );
                    }

                }
            }

            //add node for the content or catagory
            //foreach (var product in dsSanPham)
            //{
            //    nodes.Add(
            //       new SitemapNode()
            //       {
            //           Url = Url.Action("Detail", "Product", new { id = product.ID }, Request.Url.Scheme)
            //           Frequency = SitemapFrequency.Weekly,
            //           Priority = 0.8
            //       });
            //}
            return nodes;
        }
    }
}
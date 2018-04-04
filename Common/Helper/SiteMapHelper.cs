using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Common;

namespace Common.Helpers
{
    public class SiteMapHelper : Controller
    {
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = Url.AbsoluteAction("Index", "Home",null, Request.Url.Scheme),
                    Priority = 1,
                    Frequency = SitemapFrequency.Daily,
                });

            nodes.Add(
            new SitemapNode()
            {
                Url =  Url.AbsoluteAction("About", "Home", null, Request.Url.Scheme),
                Priority = 0.9
            });

            //nodes.Add(
            //   new SitemapNode()
            //   {
            //       Url = SiteConfiguration.SiteUrl + Url.Action("Contact", "Home", null, Request.Url.Scheme),
            //       Priority = 0.9
            //   });

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

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                xmlns + "url",
                new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));

                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }


    }

    public class UrlHelperExtensions
    {
        public static string AbsoluteRouteUrl(
            this UrlHelper urlHelper,
            string routeName,
            object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }
    }
}
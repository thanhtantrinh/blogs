using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Common;
using Model.Repository;

namespace OnlineShop.Helpers
{
    public static class SiteConfiguration
    {
        public static int CatalogueId { get; set; }
        public static string Name { get; set; }
        public static string SiteName { get; set; }
        public static string SiteUrl { get; set; }
        public static string EmailAdmin { get; set; }
        public static string EmailSite { get; set; }
        public static string ManagerBy { get; set; }
        public static string Address { get; set; }
        public static string Phones { get; set; }
        public static string Facebook { get; set; }
        public static string Youtube { get; set; }
        public static string Twitter { get; set; }
        public static string GooglePlus { get; set; }

        public static string DbConnectionString { get; set; }
        //public static SqlConnection DbConnection { get; set; }

        public static void StoreSettings()
        {
            LoadConnection();
            LoadSettings();
        }

        private static void LoadSettings()
        {
            Name = "N/A";
            SiteName = "Website đang phát triển";
            SiteUrl = "http://loacalhost";
            EmailAdmin = EmailSite = "thanhtan.hello@gmail.com";
            ManagerBy = "Deverloper";
            Facebook = "";
            Twitter = "";
            Youtube = "";
            GooglePlus = "";
            CatalogueId = 0;

            string catalogueid = ConfigurationManager.AppSettings["catalogueid"] ?? "0"; // name database
            
            var catalogueRepo = new CatalogueRepo(DbConnectionString);
            string message = "";
            var catalogueView = catalogueRepo.GetCatalogue(int.Parse(catalogueid), out message);
            if (catalogueView != null)
            {
                Name = catalogueView.CatalogueName;
                SiteName = catalogueView.SiteName;
                SiteUrl = catalogueView.SiteUrl;
                EmailAdmin = catalogueView.EmailAdmin;
                EmailSite = catalogueView.EmailSite;
                ManagerBy = catalogueView.ManagerBy;
                Address = !String.IsNullOrWhiteSpace(catalogueView.Address) ? catalogueView.Address : "";
                Phones = !String.IsNullOrWhiteSpace(catalogueView.Phones) ? catalogueView.Phones : "";
                CatalogueId = catalogueView.Id;
                //social netword
                Facebook = !String.IsNullOrWhiteSpace(catalogueView.Facebook) ? catalogueView.Facebook : "";
                Twitter = !String.IsNullOrWhiteSpace(catalogueView.Twitter) ? catalogueView.Twitter : "";
                Youtube = !String.IsNullOrWhiteSpace(catalogueView.Youtube) ? catalogueView.Youtube : "";
                GooglePlus = !String.IsNullOrWhiteSpace(catalogueView.GooglePlus) ? catalogueView.GooglePlus : "";
            }
        }

        private static void LoadConnection()
        {
            var dbuser = ConfigurationManager.AppSettings["dbuser"] ?? "";
            var dbpass = ConfigurationManager.AppSettings["dbpass"] ?? "";
            var dbserver = ConfigurationManager.AppSettings["dbserver"] ?? "";
            var dbcatalog = ConfigurationManager.AppSettings["dbcatalog"] ?? ""; // name database
            var conn = "data source={0};initial catalog={1};user id={2};password={3}; Pooling=True";
            DbConnectionString = String.Format(conn, dbserver, dbcatalog, dbuser, dbpass);            
            try
            {
                var Connection = new SqlConnection(DbConnectionString);                
                //Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
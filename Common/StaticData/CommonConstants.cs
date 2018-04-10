using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common
{
    public static class GroupID
    {
        public const int MEMBER = 0;
        public const int ADMIN = 1;
        public const int MOD = 2;
    }
    public static class CommonConstants
    {
        public const string USER_SESSION = "USER_SESSION";
        public const string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public const string CartSession = "CART_SESSION";
        public const string MEMBER_GROUP = "MEMBER";
        public const string ADMIN_GROUP = "ADMIN";
        public const string MOD_GROUP = "MOD";
        public static string CurrentCulture { set; get; }
    }
    public class SitemapNode
    {
        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }
    }

    public static class CacheList
    {
        public static string CacheKeyProvince = "CacheProvince";
        //public static string CacheKeyProvince = "CacheProvince";
    
    }

}

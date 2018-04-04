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
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";

        public static string MEMBER_GROUP = "MEMBER";
        public static string ADMIN_GROUP = "ADMIN";
        public static string MOD_GROUP = "MOD";
        public static string CurrentCulture { set; get; }
    }
    public class SitemapNode
    {
        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }
    }
}

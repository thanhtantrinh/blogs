using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace TestList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test send mail");

            var task = MailHelper.SendMailAsync("thanhtan.hello@gmail.com", "To Testing", "info@tantrinh.vn", "tantrinh.vn", "testing email", "Testing email from console",null,null);

            Console.WriteLine("the end");
            Console.ReadLine();
        }
    }
}

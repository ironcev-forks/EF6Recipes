using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    /// <summary>
    /// 5-6 加载派生类型上的导航属性
    /// </summary>
    public class Recipe6Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var foreman1 = new Foreman { Name = "Carl Ramsey" };
                var foreman2 = new Foreman { Name = "Nancy Ortega" };
                var phone = new Phone { Number = "817 867-5309" };
                var jobsite = new JobSite
                {
                    JobSiteName = "City Arena",
                    Address = "123 Main",
                    City = "Anytown",
                    State = "TX",
                    ZIPCode = "76082",
                    Phone = phone
                };
                jobsite.Foremen.Add(foreman1);
                jobsite.Foremen.Add(foreman2);
                var plumber = new Plumber { Name = "Jill Nichols", Email = "JNichols@plumbers.com", JobSite = jobsite };
                context.Tradesmen.Add(plumber);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                //生成的查询有点复杂，涉及了几个Join连接和子查询。
                //与此对应的，使用延迟加载，将会需要多次数据库交互，这样会带来性能损失。特别是加载多个Plumbers时。
                var plumber =context.Tradesmen.OfType<Plumber>().Include("JobSite.Phone").Include("JobSite.Foremen").First();
                Console.WriteLine("Plumber's Name: {0} ({1})", plumber.Name, plumber.Email);
                Console.WriteLine("Job Site: {0}", plumber.JobSite.JobSiteName);
                Console.WriteLine("Job Site Phone: {0}", plumber.JobSite.Phone.Number);
                Console.WriteLine("Job Site Foremen:");
                foreach (var boss in plumber.JobSite.Foremen)
                {
                    Console.WriteLine("\t{0}", boss.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}

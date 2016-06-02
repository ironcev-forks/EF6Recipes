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

namespace LoadingEntitiesAndNavigationProperties.Recipe3
{
    /// <summary>
    /// 5-3 快速查询一个单独的实体
    /// </summary>
    public class Recipe3Program
    {
        public static void Run()
        {
            int starCityId;
            int desertSunId;
            int palmTreeId;

            using (var context = new EFContext())
            {
                var starCity = new Club { Name = "Star City Chess Club", City = "New York" };
                var desertSun = new Club { Name = "Desert Sun Chess Club", City = "Phoenix" };
                var palmTree = new Club { Name = "Palm Tree Chess Club", City = "San Diego" };

                context.Clubs.Add(starCity);
                context.Clubs.Add(desertSun);
                context.Clubs.Add(palmTree);
                context.SaveChanges();

                // SaveChanges()返回每个最新创建的Club Id
                starCityId = starCity.ClubId;
                desertSunId = desertSun.ClubId;
                palmTreeId = palmTree.ClubId;
            }

            using (var context = new EFContext())
            {
                //查询数据库
                var starCity = context.Clubs.SingleOrDefault(x => x.ClubId == starCityId);
                //虽然对象已经存在上下文中，但实体框架DbContext的默认行为，仍会重新查询数据库获取记录
                //不仅如此，因为Star City实体已经加载到上下文中，DbContext不会使用数据库中的新值来替换当前的值
                starCity = context.Clubs.SingleOrDefault(x => x.ClubId == starCityId);
                //Find()方法只有在上下文中没有找需要的对象时，才去数据库中查询，所以这里不会去查询数据库
                starCity = context.Clubs.Find(starCityId);
                //由于实现体会在上下文中，所以find会查询数据库
                var desertSun = context.Clubs.Find(desertSunId);
                //NoTracking 选项将禁用指定对象的对象跟踪。没有了对象跟踪，实体框架将不在跟踪Palm Tree Club对象的改变。也不会将对象加载到上下文中。
                var palmTree = context.Clubs.AsNoTracking().SingleOrDefault(x => x.ClubId == palmTreeId);
                //因为我们使用AsNoTracking()从句指示实体框架不要在上下文中跟踪对象，所以，数据库交互就成了必须的了
                palmTree = context.Clubs.Find(palmTreeId);
                var lonesomePintId = -999;
                context.Clubs.Add(new Club { City = "Portland", Name = "Lonesome Pine", ClubId = lonesomePintId, });
                //Find()方法会返回一个最近添加到上下文中的实例，即使它还没有被保存到数据库中。所以此处不会查询数据库
                var lonesomePine = context.Clubs.Find(lonesomePintId);
                var nonexistentClub = context.Clubs.Find(10001);
            }

            Console.WriteLine("Please run this application using SQL Server Profiler...");
            Console.ReadLine();
        }
    }
}

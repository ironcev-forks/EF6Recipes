using LoadingEntitiesAndNavigationProperties.Recipe3;
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

namespace LoadingEntitiesAndNavigationProperties.Recipe4
{
    /// <summary>
    /// 5-4 查询内存对象
    /// </summary>
    public class Recipe4Program
    {
        public static void Run()
        {
            int desertSunId;

            using (var context = new EFContext())
            {
                context.Database.ExecuteSqlCommand("delete from chapter5.Clubs");
                var starCity = new Club { Name = "Star City Chess Club", City = "New York" };
                var desertSun = new Club { Name = "Desert Sun Chess Club", City = "Phoenix" };
                var palmTree = new Club { Name = "Palm Tree Chess Club", City = "San Diego" };

                context.Clubs.Add(starCity);
                context.Clubs.Add(desertSun);
                context.Clubs.Add(palmTree);

                context.SaveChanges();

                desertSunId = desertSun.ClubId;
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("\nLocal Collection Behavior");
                Console.WriteLine("=================");
                //一个对Local属性的查询，是不会产生针对数据库的SQL查询
                //现在，结果为0，因为我们还没有通过上下文对象执行一个关于Clubs的查询。记住，Local动态集合会自动地与上下文对象保持同步。
                Console.WriteLine("\nNumber of Clubs Contained in Local Collection: {0}", context.Clubs.Local.Count);
                Console.WriteLine("=================");

                Console.WriteLine("\nClubs Retrieved from Context Object");
                Console.WriteLine("=================");
                foreach (var club in context.Clubs.Take(2))
                {
                    Console.WriteLine("{0} is located in {1}", club.Name, club.City);
                }
                //没有SQL产生
                Console.WriteLine("\nClubs Contained in Context Local Collection");
                Console.WriteLine("=================");
                foreach (var club in context.Clubs.Local)
                {
                    Console.WriteLine("{0} is located in {1}", club.Name, club.City);
                }

                context.Clubs.Find(desertSunId);

                Console.WriteLine("\nClubs Retrieved from Context Object - Revisted");
                Console.WriteLine("=================");
                foreach (var club in context.Clubs)
                {
                    Console.WriteLine("{0} is located in {1}", club.Name, club.City);
                }

                Console.WriteLine("\nClubs Contained in Context Local Collection - Revisted");
                Console.WriteLine("=================");
                foreach (var club in context.Clubs.Local)
                {
                    Console.WriteLine("{0} is located in {1}", club.Name, club.City);
                }

                //获取local集合的引用 
                var localClubs = context.Clubs.Local;

                // 添加一个新的 Club
                var lonesomePintId = -999;
                localClubs.Add(new Club
                {
                    City = "Portland",
                    Name = "Lonesome Pine",
                    ClubId = lonesomePintId
                });

                // 删除 Desert Sun club
                localClubs.Remove(context.Clubs.Find(desertSunId));

                Console.WriteLine("\nClubs Contained in Context Object - After Adding and Deleting");
                Console.WriteLine("=================");
                foreach (var club in context.Clubs)
                {
                    Console.WriteLine("{0} is located in {1} with a Entity State of {2}",
                                      club.Name, club.City, context.Entry(club).State);
                }
                //Loacl集合的默认行为是，隐藏任何标记为删除的对象，因为这些对象不于有效
                //本质：访问Local集合不会产生针对底层数据库的SQL查询，访问上下中的属性集合总是会产生一个被发送到数据库中的SQL是查询。
                //总之，名为Local的属性公布的实体集，是一个动态的集合（Observale Collection)(译注：也有人译为，可观察的集合，
                //但综合该对象的作用和功能来看，个人认为译为动态集合更恰当一些），它是上下文内容的一个镜像。
                //正如本小节演示的那样，查询Local集合非常有效，因为它不会产生针对底层数据库的SQL查询。
                Console.WriteLine("\nClubs Contained in Context Local Collection - After Adding and Deleting");
                Console.WriteLine("=================");
                foreach (var club in localClubs)
                {
                    Console.WriteLine("{0} is located in {1} with a Entity State of {2}",
                                      club.Name, club.City, context.Entry(club).State);
                }
                Console.WriteLine("Press <enter> to continue...");
                Console.ReadLine();
            }
        }
    }
}

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

namespace LoadingEntitiesAndNavigationProperties.Recipe9
{
    /// <summary>
    ///5-9  关联实体过滤和排序
    /// </summary>
    public class Recipe9Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                //var hotel = new Hotel { Name = "Grand Seasons Hotel" };
                //var r101 = new Room { Rate = 79.95M, Hotel = hotel };
                //var es201 = new ExecutiveSuite { Rate = 179.95M, Hotel = hotel };
                //var es301 = new ExecutiveSuite { Rate = 299.95M, Hotel = hotel };

                //var res1 = new Reservation
                //{
                //    StartDate = DateTime.Parse("3/12/2010"),
                //    EndDate = DateTime.Parse("3/14/2010"),
                //    ContactName = "Roberta Jones",
                //    Room = es301
                //};
                //var res2 = new Reservation
                //{
                //    StartDate = DateTime.Parse("1/18/2010"),
                //    EndDate = DateTime.Parse("1/28/2010"),
                //    ContactName = "Bill Meyers",
                //    Room = es301
                //};
                //var res3 = new Reservation
                //{
                //    StartDate = DateTime.Parse("2/5/2010"),
                //    EndDate = DateTime.Parse("2/6/2010"),
                //    ContactName = "Robin Rosen",
                //    Room = r101
                //};

                //es301.Reservations.Add(res1);
                //es301.Reservations.Add(res2);
                //r101.Reservations.Add(res3);

                //hotel.Rooms.Add(r101);
                //hotel.Rooms.Add(es201);
                //hotel.Rooms.Add(es301);

                //context.Hotels.Add(hotel);
                //context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                // 假设我们拥有一个Hotel实例
                var hotel = context.Hotels.First();

                //使用Load()方法显式加载，给通过Include()获取的关联数据提供过滤的机会
                context.Entry(hotel)
                       .Collection(x => x.Rooms)
                       .Query()
                       .Include(y => y.Reservations)
                       .Where(y => y is ExecutiveSuite && y.Reservations.Any())
                       .Load();

                Console.WriteLine("Executive Suites for {0} with reservations", hotel.Name);
                //这里有问题。
                foreach (var room in hotel.Rooms)
                {
                    Console.WriteLine("\nExecutive Suite {0} is {1} per night", room.RoomId,
                                      room.Rate.ToString("C"));
                    Console.WriteLine("Current reservations are:");
                    foreach (var res in room.Reservations.OrderBy(r => r.StartDate))
                    {
                        Console.WriteLine("\t{0} thru {1} ({2})", res.StartDate.ToShortDateString(),
                                          res.EndDate.ToShortDateString(), res.ContactName);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
        }
    }
}

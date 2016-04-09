using QueryingAnEntityDataModel.Recipe5;
using QueryingAnEntityDataModel.Recipe7;
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

namespace QueryingAnEntityDataModel.Recipe15
{
    /// <summary>
    /// 3-15  使用多属性分组
    /// </summary>
    public class Recipe15Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // delete previous test data
                context.Database.ExecuteSqlCommand("delete from chapter3.events");
                // add new test data
                context.Events.Add(new Event
                {
                    Name = "TechFest 2010",
                    State = "TX",
                    City = "Dallas"
                });
                context.Events.Add(new Event
                {
                    Name = "Little Blue River Festival",
                    State = "MO",
                    City = "Raytown"
                });
                context.Events.Add(new Event
                {
                    Name = "Fourth of July Fireworks",
                    State = "MO",
                    City = "Raytown"
                });
                context.Events.Add(new Event
                {
                    Name = "BBQ Ribs Championship",
                    State = "TX",
                    City = "Dallas"
                });
                context.Events.Add(new Event
                {
                    Name = "Thunder on the Ohio",
                    State = "KY",
                    City = "Louisville"
                });
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {

                Console.WriteLine("Using LINQ");
                //var results = from e in context.Events
                //                  // create annonymous type to encapsulate composite
                //                  // sort key of State and City
                //              group e by new { e.State, e.City } into g
                //              select new
                //              {
                //                  State = g.Key.State,
                //                  City = g.Key.City,
                //                  Events = g
                //              };
                var results = context.Events.GroupBy(e => new { e.State, e.City })
                    .Select(g => new
                    {
                        State = g.Key.State,
                        City = g.Key.City,
                        Events = g
                    });
                Console.WriteLine("Events by State and City...");
                foreach (var item in results)
                {
                    Console.WriteLine("{0}, {1}", item.City, item.State);
                    foreach (var ev in item.Events)
                    {
                        Console.WriteLine("\t{0}", ev.Name);
                    }
                }
            }
            using (var context = new EFContext())
            {

                Console.WriteLine("\nUsing Entity SQL");
                var esql = @"select e.State, e.City, GroupPartition(e) as Events
                            from Events as e
                            group by e.State, e.City";
                var records =((IObjectContextAdapter)context).ObjectContext.CreateQuery<DbDataRecord>(esql);
                Console.WriteLine("Events by State and City...");
                foreach (var rec in records)
                {
                    Console.WriteLine("{0}, {1}", rec["City"], rec["State"]);
                    var events = (List<Event>)rec["Events"];
                    foreach (var ev in events)
                    {
                        Console.WriteLine("\t{0}", ev.Name);
                    }
                }
            }
        }

    }
}

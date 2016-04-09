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

namespace QueryingAnEntityDataModel.Recipe9
{
    /// <summary>
    /// 3-9过滤关联实体
    /// </summary>
    public class Recipe9Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // 删除之前的测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.accidents");
                context.Database.ExecuteSqlCommand("delete from chapter3.workers");
                // 添加新的测试数据
                var worker1 = new Worker { Name = "John Kearney" };
                var worker2 = new Worker { Name = "Nancy Roberts" };
                var worker3 = new Worker { Name = "Karla Gibbons" };
                context.Accidents.Add(new Accident
                {
                    Description = "Cuts and contusions",
                    Severity = 3,
                    Worker = worker1
                });
                context.Accidents.Add(new Accident
                {
                    Description = "Broken foot",
                    Severity = 4,
                    Worker = worker1
                });
                context.Accidents.Add(new Accident
                {
                    Description = "Fall, no injuries",
                    Severity = 1,
                    Worker = worker2
                });
                context.Accidents.Add(new Accident
                {
                    Description = "Minor burn",
                    Severity = 3,
                    Worker = worker2
                });
                context.Accidents.Add(new Accident
                {
                    Description = "Back strain",
                    Severity = 2,
                    Worker = worker3
                });
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                // 显式禁用延迟加载
                context.Configuration.LazyLoadingEnabled = false;
                //var query = from w in context.Workers
                //            select new
                //            {
                //                Worker = w,
                //                Accidents = w.Accidents.Where(a => a.Severity > 2)
                //            };
                var query = context.Workers.Select(p => new { Worker = p, Accidents = p.Accidents.Where(a => a.Severity > 2) });
                query.ToList();
                var workers = query.Select(r => r.Worker);
                Console.WriteLine("Workers with serious accidents...");
                foreach (var worker in workers)
                {
                    Console.WriteLine("{0} had the following accidents", worker.Name);
                    if (worker.Accidents.Count == 0)
                        Console.WriteLine("\t--None--");
                    foreach (var accident in worker.Accidents)
                    {
                        Console.WriteLine("\t{0}, severity: {1}",
                              accident.Description, accident.Severity.ToString());
                    }
                }
                
            }

        }

    }
}

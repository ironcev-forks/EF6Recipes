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

namespace ModelingAndInheritance.Recipe2
{
    /// <summary>
    /// 6-2  将链接表表示成一个实体
    /// </summary>
    public class Recipe2Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var worker = new Worker { Name = "Jim" };
                var task = new Task { Title = "Fold Envelopes" };
                var workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.Add(workertask);
                task = new Task { Title = "Mail Letters" };
                workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.Add(workertask);
                worker = new Worker { Name = "Sara" };
                task = new Task { Title = "Buy Envelopes" };
                workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.Add(workertask);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("Workers and Their Tasks");
                Console.WriteLine("=======================");
                foreach (var worker in context.Workers)
                {
                    Console.WriteLine("\n{0}'s tasks:", worker.Name);
                    foreach (var wt in worker.WorkerTasks)
                    {
                        Console.WriteLine("\t{0}", wt.Task.Title);
                    }
                }
            }
        }
    }
}

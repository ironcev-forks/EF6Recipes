using QueryingAnEntityDataModel.Recipe5;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe6
{
    /// <summary>
    /// 3-6在查询中设置默认值
    /// </summary>
    public class Recipe6Program
    {
        public static void Run()
        {
            //using (var context = new EFContext())
            //{
            //    // 删除之前的测试数据
            //    context.Database.ExecuteSqlCommand("delete from chapter3.employees");
            //    // 添加新的测试数据
            //    context.Employees.Add(new Employee
            //    {
            //        Name = "Robin Rosen",
            //        YearsWorked = 3
            //    });
            //    context.Employees.Add(new Employee { Name = "John Hancock" });
            //    context.SaveChanges();
            //}

            using (var context = new EFContext())
            {
                Console.WriteLine("Employees (using LINQ)");
                //var employees = from e in context.Employees
                //                select new { Name = e.Name, YearsWorked = e.YearsWorked ?? 0 };
                var employees = context.Employees.Select(p => new {Name=p.Name, YearsWorked=p.YearsWorked??0 });
                              
                foreach (var employee in employees)
                {
                    Console.WriteLine("{0}, years worked: {1}", employee.Name,
                        employee.YearsWorked);
                }
            }
            //tangwh 这个无法运行
            //using (var context = new EFContext())
            //{
            //    Console.WriteLine("\nEmployees (using ESQL w/named constructor)");
            //    var esql = @"select value Employee(e.EmployeeId, 
            //          e.Name,
            //          case when e.YearsWorked is null then 0
            //               else e.YearsWorked end) 
            //        from Employees as e";


            //    var employees = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Employee>(esql);
            //    foreach (var employee in employees)
            //    {
            //        Console.WriteLine("{0}, years worked: {1}", employee.Name,
            //            employee.YearsWorked.ToString());
            //    }
            //}
        }
    }
}

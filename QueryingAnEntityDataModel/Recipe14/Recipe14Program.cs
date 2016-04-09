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

namespace QueryingAnEntityDataModel.Recipe14
{
    /// <summary>
    /// 3-14 结果集扁平化
    /// </summary>
    public class Recipe14Program
    {
        public static void Run()
        {
            //using (var context = new EFContext())
            //{
            //    // delete previous test data
            //    context.Database.ExecuteSqlCommand("delete from chapter3.associatesalaries");
            //    context.Database.ExecuteSqlCommand("delete from chapter3.associates");
            //    // add new test data
            //    var assoc1 = new Associate { Name = "Janis Roberts" };
            //    var assoc2 = new Associate { Name = "Kevin Hodges" };
            //    var assoc3 = new Associate { Name = "Bill Jordan" };
            //    var salary1 = new AssociateSalary
            //    {
            //        Salary = 39500M,
            //        SalaryDate = DateTime.Parse("8/4/09")
            //    };
            //    var salary2 = new AssociateSalary
            //    {
            //        Salary = 41900M,
            //        SalaryDate = DateTime.Parse("2/5/10")
            //    };
            //    var salary3 = new AssociateSalary
            //    {
            //        Salary = 33500M,
            //        SalaryDate = DateTime.Parse("10/08/09")
            //    };
            //    assoc2.AssociateSalaries.Add(salary1);
            //    assoc2.AssociateSalaries.Add(salary2);
            //    assoc3.AssociateSalaries.Add(salary3);
            //    context.Associates.Add(assoc1);
            //    context.Associates.Add(assoc2);
            //    context.Associates.Add(assoc3);
            //    context.SaveChanges();
            //}
            using (var context = new EFContext())
            {

                Console.WriteLine("Using LINQ...");
                var allHistory = from a in context.Associates
                                 from ah in a.AssociateSalaries.DefaultIfEmpty()
                                 orderby a.Name
                                 select new
                                 {
                                     Name = a.Name,
                                     Salary = (decimal?)ah.Salary,
                                     Date = (DateTime?)ah.SalaryDate
                                 };
                //内联接
                //var allHistory = context.Associates.Join(context.AssociateSalaries,
                //    p => p,
                //    s => s.Associate,
                //    (p, s) =>new {
                //        Name=p.Name,
                //        Salary = (decimal?)s.Salary,
                //        Date = (DateTime?)s.SalaryDate
                //    });
                Console.WriteLine("Associate Salary History");
                foreach (var history in allHistory)
                {
                    if (history.Salary.HasValue)
                        Console.WriteLine("{0} Salary on {1} was {2}", history.Name,
                        history.Date.Value.ToShortDateString(),
                        history.Salary.Value.ToString("C"));
                    else
                        Console.WriteLine("{0} --", history.Name);
                }
                //方法二，使用扩展方法
                //var associates = context.Associates.GroupJoin(context.AssociateSalaries,
                //    p => p,
                //    s => s.Associate,
                //    (p, s) => new
                //    {
                //        Name = p.Name,
                //        Salaries = p.AssociateSalaries
                //    });
                //foreach (var associate in associates)
                //{
                //    if (associate.Salaries != null && associate.Salaries.Count > 0)
                //    {
                //        foreach (var salary in associate.Salaries)
                //        {
                //            Console.WriteLine("{0} Salary on {1} was {2}", associate.Name,salary.SalaryDate.ToShortDateString(),salary.Salary.ToString("C"));
                //        }

                //    }
                //    else
                //        Console.WriteLine("{0} --", associate.Name);
                //}
            }
            using (var context = new EFContext())
            {

                Console.WriteLine("\nUsing Entity SQL...");
                var esql = @"select a.Name, h.Salary, h.SalaryDate
                                from Associates as a outer apply
                                a.AssociateSalaries as h order by a.Name";
                var allHistory =
                ((IObjectContextAdapter)context).ObjectContext.CreateQuery<DbDataRecord>(esql);
                Console.WriteLine("Associate Salary History");
                foreach (var history in allHistory)
                {
                    if (history["Salary"] != DBNull.Value)
                        Console.WriteLine("{0} Salary on {1:d} was {2:c}", history["Name"],
                        history["SalaryDate"], history["Salary"]);
                    else
                        Console.WriteLine("{0} --", history["Name"]);
                }
            }
        }

    }
}

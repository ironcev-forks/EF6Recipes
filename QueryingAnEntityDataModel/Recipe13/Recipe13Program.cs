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

namespace QueryingAnEntityDataModel.Recipe13
{
    /// <summary>
    /// 3-13 按日期分组
    /// </summary>
    public class Recipe13Program
    {
        public static void Run()
        {
            //using (var context = new EFContext())
            //{
            //    context.Database.ExecuteSqlCommand("delete from chapter3.registrations");
            //    context.Registrations.Add(new Registration
            //    {
            //        StudentName = "Jill Rogers",
            //        RegistrationDate = DateTime.Parse("12/03/2009 9:30 pm")
            //    });
            //    context.Registrations.Add(new Registration
            //    {
            //        StudentName = "Steven Combs",
            //        RegistrationDate = DateTime.Parse("12/03/2009 10:45 am")
            //    });
            //    context.Registrations.Add(new Registration
            //    {
            //        StudentName = "Robin Rosen",
            //        RegistrationDate = DateTime.Parse("12/04/2009 11:18 am")
            //    });
            //    context.Registrations.Add(new Registration
            //    {
            //        StudentName = "Allen Smith",
            //        RegistrationDate = DateTime.Parse("12/04/2009 3:31 pm")
            //    });
            //    context.Registrations.Add(new Registration
            //    {
            //        StudentName = "Tang WanHong",
            //    });
            //    context.SaveChanges();
            //}
            using (var context = new EFContext())
            {

                //leverage built-in TruncateTime function to extract date portion
                //var groups = from r in context.Registrations
                //             group r by DbFunctions.TruncateTime(r.RegistrationDate) into g
                //             select g;
                var groups = context.Registrations.GroupBy(p => DbFunctions.TruncateTime(p.RegistrationDate));
                foreach (var element in groups)
                {
                    if (element.Key==null)
                    {
                        Console.WriteLine("Registrations for {0}","NULL");
                    }
                    else
                    {
                        Console.WriteLine("Registrations for {0}", ((DateTime)element.Key).ToShortDateString());
                    }
                    foreach (var registration in element)
                    {
                        Console.WriteLine("\t{0}", registration.StudentName);
                    }
                }


            }
        }

    }
}

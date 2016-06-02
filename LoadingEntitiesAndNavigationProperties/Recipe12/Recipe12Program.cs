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

namespace LoadingEntitiesAndNavigationProperties.Recipe12
{
    /// <summary>
    ///5-12  显示加载关联实体
    /// </summary>
    public class Recipe12Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var doc1 = new Doctor { Name = "Joan Meyers" };
                var doc2 = new Doctor { Name = "Steven Mills" };
                var pat1 = new Patient { Name = "Bill Rivers" };
                var pat2 = new Patient { Name = "Susan Stevenson" };
                var pat3 = new Patient { Name = "Roland Marcy" };

                var app1 = new Appointment
                {
                    Date = DateTime.Today,
                    Doctor = doc1,
                    Fee = 109.92M,
                    Patient = pat1,
                    Reason = "Checkup"
                };
                var app2 = new Appointment
                {
                    Date = DateTime.Today,
                    Doctor = doc2,
                    Fee = 129.87M,
                    Patient = pat2,
                    Reason = "Arm Pain"
                };
                var app3 = new Appointment
                {
                    Date = DateTime.Today,
                    Doctor = doc1,
                    Fee = 99.23M,
                    Patient = pat3,
                    Reason = "Back Pain"
                };

                context.Appointments.Add(app1);
                context.Appointments.Add(app2);
                context.Appointments.Add(app3);

                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                // 禁用延迟加载，因为我们要显式加载子实体
                context.Configuration.LazyLoadingEnabled = false;

                var doctorJoan = context.Doctors.First(o => o.Name == "Joan Meyers");

                if (!context.Entry(doctorJoan).Collection(x => x.Appointments).IsLoaded)
                {
                    context.Entry(doctorJoan).Collection(x => x.Appointments).Load();
                    Console.WriteLine("Dr. {0}'s appointments were explicitly loaded.",
                                      doctorJoan.Name);
                }

                Console.WriteLine("Dr. {0} has {1} appointment(s).",
                                  doctorJoan.Name,
                                  doctorJoan.Appointments.Count());

                foreach (var appointment in context.Appointments)
                {
                    if (!context.Entry(appointment).Reference(x => x.Doctor).IsLoaded)
                    {
                        context.Entry(appointment).Reference(x => x.Doctor).Load();
                        Console.WriteLine("Dr. {0} was explicitly loaded.",
                                          appointment.Doctor.Name);
                    }
                    else
                        Console.WriteLine("Dr. {0} was already loaded.",
                                          appointment.Doctor.Name);
                }

                Console.WriteLine("There are {0} appointments for Dr. {1}",
                                  doctorJoan.Appointments.Count(),
                                  doctorJoan.Name);

                doctorJoan.Appointments.Clear();

                Console.WriteLine("Collection clear()'ed");
                Console.WriteLine("There are now {0} appointments for Dr. {1}",
                                  doctorJoan.Appointments.Count(),
                                  doctorJoan.Name);
                //产生查询，有数据库交互
                context.Entry(doctorJoan).Collection(x => x.Appointments).Load();

                Console.WriteLine("Collection loaded()'ed");

                Console.WriteLine("There are now {0} appointments for Dr. {1}",
                                  doctorJoan.Appointments.Count().ToString(),
                                  doctorJoan.Name);

                //目前，DbContext 没有API去刷新实体，但底层的ObjectContext有，执行下面的动作。
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var objectSet = objectContext.CreateObjectSet<Appointment>();
                objectSet.MergeOption = MergeOption.OverwriteChanges;
                objectSet.Load();

                Console.WriteLine("Collection loaded()'ed with MergeOption.OverwriteChanges");

                Console.WriteLine("There are now {0} appointments for Dr. {1}",
                                  doctorJoan.Appointments.Count(),
                                  doctorJoan.Name);
            }


            //演示先加载部分实体集合，然后再加载剩下的
            using (var context = new EFContext())
            {
                // 禁用延迟加载，因为我们要显式加载子实体
                context.Configuration.LazyLoadingEnabled = false;

                //加载第一个doctor然后只附加一个appointment
                var doctorJoan = context.Doctors.First(o => o.Name == "Joan Meyers");
                context.Entry(doctorJoan).Collection(x => x.Appointments).Query().Take(1).Load();

                //注意，这里IsLoaded返回False,因为所有的实体还没有被加载到上下文
                var appointmentsLoaded = context.Entry(doctorJoan).Collection(x => x.Appointments).IsLoaded;

                Console.WriteLine("Dr. {0} has {1} appointments loaded.",
                                  doctorJoan.Name,
                                  doctorJoan.Appointments.Count());

                //当我需要加载剩下的appointments,只需要简单的调用Load()来加载它们
                context.Entry(doctorJoan).Collection(x => x.Appointments).Load();
                Console.WriteLine("Dr. {0} has {1} appointments loaded.",
                                  doctorJoan.Name,
                                  doctorJoan.Appointments.Count());
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}

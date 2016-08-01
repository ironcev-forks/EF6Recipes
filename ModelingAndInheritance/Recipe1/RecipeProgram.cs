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

namespace ModelingAndInheritance.Recipe1
{
    /// <summary>
    /// 6-1  获取多对多关联中的链接表
    /// </summary>
    public class RecipeProgram
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var org = new Organizer { Name = "Community Charity" };
                var evt = new Event { Name = "Fundraiser" };
                org.Events.Add(evt);
                context.Organizers.Add(org);
                org = new Organizer { Name = "Boy Scouts" };
                evt = new Event { Name = "Eagle Scout Dinner" };
                org.Events.Add(evt);
                context.Organizers.Add(org);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                var evsorg1 = from ev in context.Events
                              from organizer in ev.Organizers
                              select new { ev.EventId, organizer.OrganizerId };
                Console.WriteLine("Using nested from clauses...");
                foreach (var pair in evsorg1)
                {
                    Console.WriteLine("EventId {0}, OrganizerId {1}",
                                       pair.EventId,
                                       pair.OrganizerId);
                }

                var evsorg2 = context.Events.SelectMany(e => e.Organizers,(ev, org) => new { ev.EventId, org.OrganizerId });
                Console.WriteLine("\nUsing SelectMany()");
                foreach (var pair in evsorg2)
                {
                    Console.WriteLine("EventId {0}, OrganizerId {1}",
                                       pair.EventId, pair.OrganizerId);
                }
            }
        }
    }
}

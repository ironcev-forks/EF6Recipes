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

namespace QueryingAnEntityDataModel.Recipe16
{
    /// <summary>
    /// 3-16  过滤中使用位操作
    /// </summary>
    public class Recipe16Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // delete previous test data
                context.Database.ExecuteSqlCommand("delete from chapter3.patrons");
                // add new test data
                context.Patrons.Add(new Patron
                {
                    Name = "Jill Roberts",
                    SponsorType = (int)SponsorTypes.ContributesMoney
                });
                context.Patrons.Add(new Patron
                {
                    Name = "Ryan Keyes",
                    // note the useage of the bitwise OR operator: '|'
                    SponsorType = (int)(SponsorTypes.ContributesMoney |SponsorTypes.IsABoardMember)
                });
                context.Patrons.Add(new Patron
                {
                    Name = "Karen Rosen",
                    SponsorType = (int)SponsorTypes.Volunteers
                });
                context.Patrons.Add(new Patron
                {
                    Name = "Steven King",
                    SponsorType = (int)(SponsorTypes.ContributesMoney |SponsorTypes.Volunteers)
                });
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {

                Console.WriteLine("Using LINQ...");
                //var sponsors = from p in context.Patrons
                //                   // note the useage of the bitwise AND operator: '&'
                //               where (p.SponsorType & (int)SponsorTypes.ContributesMoney) != 0
                //               select p;
                var sponsors = context.Patrons.Where(p => (p.SponsorType & (int)SponsorTypes.ContributesMoney) != 0);
                Console.WriteLine("Patrons who contribute money");
                foreach (var sponsor in sponsors)
                {
                    Console.WriteLine("\t{0}", sponsor.Name);
                }
            }
            using (var context = new EFContext())
            {

                Console.WriteLine("\nUsing Entity SQL...");
                var esql = @"select value p from Patrons as p
                            where BitWiseAnd(p.SponsorType, @type) <> 0";
                var sponsors = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Patron>(esql,
                new ObjectParameter("type", (int)SponsorTypes.ContributesMoney));
                Console.WriteLine("Patrons who contribute money");
                foreach (var sponsor in sponsors)
                {
                    Console.WriteLine("\t{0}", sponsor.Name);
                }
            }
        }

    }
}

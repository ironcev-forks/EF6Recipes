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

namespace QueryingAnEntityDataModel.Recipe7
{
    /// <summary>
    /// 3-7从存储过程中返回多结果集
    /// </summary>
    public class Recipe7Program
    {
        public static void Run()
        {
            //using (var context = new EFContext())
            //{
            //    var job1 = new Job { JobDetails = "Re-surface Parking Log" };
            //    var job2 = new Job { JobDetails = "Build Driveway" };
            //    job1.Bids.Add(new Bid { Amount = 948M, Bidder = "ABC Paving" });
            //    job1.Bids.Add(new Bid { Amount = 1028M, Bidder = "TopCoat Paving" });
            //    job2.Bids.Add(new Bid { Amount = 502M, Bidder = "Ace Concrete" });
            //    context.Jobs.Add(job1);
            //    context.Jobs.Add(job2);
            //    context.SaveChanges();
            //}

            using (var context = new EFContext())
            {
                //var cs = @"Data Source=.;Initial Catalog=EFRecipes;Integrated Security=True";
                var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EFconnectionString"].ConnectionString);
                var cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Chapter3.GetBidDetails";
                conn.Open();
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var jobs = ((IObjectContextAdapter)context).ObjectContext.Translate<Job>(reader, "Jobs", MergeOption.AppendOnly).ToList();
                reader.NextResult();
                ((IObjectContextAdapter)context).ObjectContext.Translate<Bid>(reader, "Bids", MergeOption.AppendOnly).ToList();
                foreach (var job in jobs)
                {
                    Console.WriteLine("\nJob: {0}", job.JobDetails);
                    foreach (var bid in job.Bids)
                    {
                        Console.WriteLine("\tBid: {0} from {1}",
                            bid.Amount.ToString(), bid.Bidder);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe2
{
    /// <summary>
    /// 3-2使用原生SQL语句更新
    /// </summary>
    public class Recipe2Program
    {
        public static void Run()
        {
            using (var context=new EFContext())
            {
                context.Database.ExecuteSqlCommand("delete from Chapter3.Payments");
            }
            using (var context = new EFContext())
            {
                string strSql = "insert into Chapter3.Payments(Amount,Vendor) values(@Amount,@Vendor)";
                var parameters = new DbParameter[] {
                    new SqlParameter { ParameterName="Amount",Value= 99.97M },
                     new SqlParameter { ParameterName="Vendor",Value= "Ace Plumbing"}
                };
                var rowCount = context.Database.ExecuteSqlCommand(strSql,parameters);
                //parameters = new DbParameter[]
                // {
                //        new SqlParameter {ParameterName = "Amount", Value = 43.83M},
                //        new SqlParameter
                //            {
                //                ParameterName = "Vendor",
                //                Value = "Joe's Trash Service"
                //            }
                // };

                //rowCount += context.Database.ExecuteSqlCommand(strSql, parameters);
                //Console.WriteLine("{0} rows inserted", rowCount.ToString());

                //这里使用@p0这样的参数占位符，ado.net为自动为我们创建参数对象
                strSql = "insert into Chapter3.Payments(Amount,Vendor) values(@p0,@p1)";
                rowCount += context.Database.ExecuteSqlCommand(strSql, 43.83M, "Joe's Trash Service");
                Console.WriteLine("{0} rows inserted", rowCount.ToString());
            }
            // retrieve and materialize data 
            using (var context = new EFContext())
            {
                Console.WriteLine("Payments");
                Console.WriteLine("========");
                foreach (var payment in context.Payments)
                {
                    Console.WriteLine("Paid {0} to {1}", payment.Amount.ToString(),
                    payment.Vendor);
                }
            }
        }
    }
}

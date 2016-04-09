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

namespace QueryingAnEntityDataModel.Recipe4
{
    /// <summary>
    /// 3-4使用实体SQL查询模型
    /// </summary>
    public class Recipe4Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // 删除测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.customers");
                // 添加新的测试数据
                var cus1 = new Customer
                {
                    Name = "Robert Stevens",
                    Email = "rstevens@mymail.com"
                };
                var cus2 = new Customer
                {
                    Name = "Julia Kerns",
                    Email = "julia.kerns@abc.com"
                };
                var cus3 = new Customer
                {
                    Name = "Nancy Whitrock",
                    Email = "nrock@myworld.com"
                };
                context.Customers.Add(cus1);
                context.Customers.Add(cus2);
                context.Customers.Add(cus3);
                context.SaveChanges();

            }
            //使用ObjectContext对象中的 object services
            using (var context = new EFContext())
            {
                Console.WriteLine("Querying Customers with eSQL Leveraging Object Services...");
                string esql = "select value c from Customers as c";
                // 将DbContext转换为底层的ObjectContext, 因为DbContext没有提供对Entity SQL查询的支持
                var customers = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Customer>(esql);
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}'s email is: {1}",
                                       customer.Name, customer.Email);
                }
            }

            Console.WriteLine(System.Environment.NewLine);
            //tangwh 使用这种方式连接字符串要怎么写
            //使用 EntityClient
            //using (var conn = new EntityConnection("name=EFRecipesEntities"))
            //{
            //    Console.WriteLine("Customers Customers with eSQL Leveraging Entity Client...");
            //    var cmd = conn.CreateCommand();
            //    conn.Open();
            //    cmd.CommandText = "select value c from Chapter3.Customers as c";
            //    using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine("{0}'s email is: {1}",
            //                               reader.GetString(1), reader.GetString(2));
            //        }
            //    }
            //}

            // using object services without the VALUE keyword
            using (var context = new EFContext())
            {
                Console.WriteLine("Customers...");
                string esql = "select c.Name, c.Email from Customers as c";
                var records = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<DbDataRecord>(esql);
                foreach (var record in records)
                {
                    var name = record[0] as string;
                    var email = record[1] as string;
                    Console.WriteLine("{0}'s email is: {1}", name, email);
                }
            }
            Console.WriteLine();
            //using EntityClient without the VALUE keyword
            //using (var conn = new EntityConnection("name=EFRecipesEntities"))
            //{
            //    Console.WriteLine("Customers...");
            //    var cmd = conn.CreateCommand();
            //    conn.Open();
            //    cmd.CommandText = @"select c.Name, C.Email from EFRecipesEntities.Customers as c";
            //    using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine("{0}'s email is: {1}",
            //            reader.GetString(0), reader.GetString(1));
            //        }
            //    }
            //}

        }
    }
}

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

namespace QueryingAnEntityDataModel.Recipe8
{
    /// <summary>
    /// 3-8与列表值比较
    /// </summary>
    public class Recipe8Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // 删除之前的测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.books");
                context.Database.ExecuteSqlCommand("delete from chapter3.categories");
                // 添加新的测试数据            
                var cat1 = new Category { Name = "Programming" };
                var cat2 = new Category { Name = "Databases" };
                var cat3 = new Category { Name = "Operating Systems" };
                context.Books.Add(new Book { Title = "F# In Practice", Category = cat1 });
                context.Books.Add(new Book { Title = "The Joy of SQL", Category = cat2 });
                context.Books.Add(new Book
                {
                    Title = "Windows 7: The Untold Story",
                    Category = cat3
                });
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("Books (using LINQ)");
                var cats = new List<string> { "Programming", "Databases" };
                //var books = from b in context.Books
                //            where cats.Contains(b.Category.Name)
                //            select b;
                var books = context.Books.Where(p => cats.Contains(p.Category.Name));
                foreach (var book in books)
                {
                    Console.WriteLine("'{0}' is in category: {1}", book.Title,
                        book.Category.Name);
                }
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("\nBooks (using eSQL)");
                var esql = @"select value b from Books as b
                 where b.Category.Name in {'Programming','Databases'}";
                var books = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Book>(esql);
                foreach (var book in books)
                {
                    Console.WriteLine("'{0}' is in category: {1}", book.Title,
                        book.Category.Name);
                }
            }
        }

    }
}

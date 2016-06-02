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

namespace ModelingAndInheritance.Recipe5
{
    /// <summary>
    ///6-5  使用TPH建模自引用关系
    /// </summary>
    public class Recipe5Program
    {
        public static void Run()
        {

            using (var context = new EFContext())
            {
                var book = new Category { Name = "Books" };
                var fiction = new Category { Name = "Fiction", ParentCategory = book };
                var nonfiction = new Category { Name = "Non-Fiction", ParentCategory = book };
                var novel = new Category { Name = "Novel", ParentCategory = fiction };
                var history = new Category { Name = "History", ParentCategory = nonfiction };
                context.Categories.Add(novel);
                context.Categories.Add(history);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                var root = context.Categories.Where(o => o.Name == "Books").First();
                Console.WriteLine("Parent category is {0}, subcategories are:", root.Name);
                foreach (var sub in context.GetSubCategories(root.CategoryId))
                {
                    Console.WriteLine("\t{0}", sub.Name);
                }
            }
        }

    }
}

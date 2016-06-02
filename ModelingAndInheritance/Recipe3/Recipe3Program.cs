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

namespace ModelingAndInheritance.Recipe3
{
    /// <summary>
    /// 6-3  自引用的多对多关系建模
    /// </summary>
    public class Recipe3Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                //var product1 = new Product { Name = "Pole", Price = 12.97M };
                //var product2 = new Product { Name = "Tent", Price = 199.95M };
                //var product3 = new Product { Name = "Ground Cover", Price = 29.95M };
                //product2.RelatedProducts.Add(product3);
                //product1.RelatedProducts.Add(product2);
                //context.Products.Add(product1);
                //context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                var product2 = context.Products.First(p => p.Name == "Tent");
                Console.WriteLine("Product: {0} ... {1}", product2.Name,
                                   product2.Price.ToString("C"));
                Console.WriteLine("Related Products");
                foreach (var prod in product2.RelatedProducts)
                {
                    Console.WriteLine("\t{0} ... {1}", prod.Name, prod.Price.ToString("C"));
                }
                foreach (var prod in product2.OtherRelatedProducts)
                {
                    Console.WriteLine("\t{0} ... {1}", prod.Name, prod.Price.ToString("C"));
                }
            }
            using (var context = new EFContext())
            {
                var product1 = context.Products.First(p => p.Name == "Pole");
                Dictionary<int, Product> t = new Dictionary<int, Product>();
                GetRelated(context, product1, t);
                Console.WriteLine("Products related to {0}", product1.Name);
                foreach (var key in t.Keys)
                {
                    Console.WriteLine("\t{0}", t[key].Name);
                }
            }

        }

        static void GetRelated(DbContext context, Product p, Dictionary<int, Product> t)
        {
            context.Entry(p).Collection(ep => ep.RelatedProducts).Load();
            foreach (var relatedProduct in p.RelatedProducts)
            {
                if (!t.ContainsKey(relatedProduct.ProductId))
                {
                    t.Add(relatedProduct.ProductId, relatedProduct);
                    GetRelated(context, relatedProduct, t);
                }
            }
            context.Entry(p).Collection(ep => ep.OtherRelatedProducts).Load();
            foreach (var otherRelated in p.OtherRelatedProducts)
            {
                if (!t.ContainsKey(otherRelated.ProductId))
                {
                    t.Add(otherRelated.ProductId, otherRelated);
                    GetRelated(context, otherRelated, t);
                }
            }
        }
    }
}

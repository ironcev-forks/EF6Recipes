using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe5
{
    /// <summary>
    ///2-5 使用Code First建模自引用关系
    /// </summary>
    public class Recipe5Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var louvre = new PictureCategory { Name = "Louvre" };
                var child = new PictureCategory { Name = "Egyptian Antiquites" };
                louvre.Subcategories.Add(child);
                child = new PictureCategory { Name = "Sculptures" };
                louvre.Subcategories.Add(child);
                child = new PictureCategory { Name = "Paintings" };
                louvre.Subcategories.Add(child);
                var paris = new PictureCategory { Name = "Paris" };
                paris.Subcategories.Add(louvre);
                var vacation = new PictureCategory { Name = "Summer Vacation" };
                vacation.Subcategories.Add(paris);
                context.PictureCategories.Add(vacation);
                var myPicture = new PictureCategory { Name = "MyPicture" };
                vacation.Subcategories.Add(myPicture);
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                var roots = context.PictureCategories.Where(p => p.ParentCategory == null).ToList();
                roots.ForEach(r => print(r, 0));
            }
        }

        private static void print(PictureCategory r, int level)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', level);
            sb.Append(r.Name);
            Console.WriteLine(sb.ToString());
            r.Subcategories.ForEach(s => print(s, level+1));
        }
    }
}

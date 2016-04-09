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

namespace QueryingAnEntityDataModel.Recipe11
{
    /// <summary>
    /// 3-11通过派生类排序
    /// </summary>
    public class Recipe11Program
    {
        public static void Run()
        {
            //using (var context = new EFContext())
            //{
            //    context.Medias.Add(new Article
            //    {
            //        Title = "Woodworkers' Favorite Tools"
            //    });
            //    context.Medias.Add(new Article
            //    {
            //        Title = "Building a Cigar Chair"
            //    });
            //    context.Medias.Add(new Video
            //    {
            //        Title = "Upholstering the Cigar Chair"
            //    });
            //    context.Medias.Add(new Video
            //    {
            //        Title = "Applying Finish to the Cigar Chair"
            //    });
            //    context.Medias.Add(new Picture
            //    {
            //        Title = "Photos of My Cigar Chair"
            //    });
            //    context.Medias.Add(new Video
            //    {
            //        Title = "Tour of My Woodworking Shop"
            //    });
            //    context.SaveChanges();
            //}
            using (var context = new EFContext())
            {
                var allMedia = from m in context.Medias
                               let mediatype = m is Article ? 1 :m is Video ? 2 : 3
                               orderby mediatype
                               select m;
                Console.WriteLine("All Media sorted by type...");
                foreach (var media in allMedia)
                {
                    Console.WriteLine("Title: {0} [{1}]", media.Title, media.GetType().Name);
                }
                Console.WriteLine("All Picture：");
                foreach (var media in allMedia.OfType<Picture>())
                {
                    Console.WriteLine("Title: {0} [{1}]", media.Title, media.GetType().Name);
                }
            }
        }

    }
}

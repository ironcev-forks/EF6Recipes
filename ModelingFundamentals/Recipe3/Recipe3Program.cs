using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe3
{
    /// <summary>
    /// 2-3 无载荷（with NO Payload）的多对多关系建模
    /// </summary>
    public class Recipe3Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                context.Database.ExecuteSqlCommand("delete from chapter2.Artists");
                context.Database.ExecuteSqlCommand("delete from chapter2.Albums");
                // add an artist with two albums
                var artist = new Artist { FirstName = "Alan", LastName = "Jackson" };
                var album1 = new Album { AlbumName = "Drive" };
                var album2 = new Album { AlbumName = "Live at Texas Stadium" };
                artist.Albums.Add(album1);
                artist.Albums.Add(album2);
                context.Artists.Add(artist);
                // add an album for two artists
                var artist1 = new Artist { FirstName = "Tobby", LastName = "Keith" };
                var artist2 = new Artist { FirstName = "Merle", LastName = "Haggard" };
                var album = new Album { AlbumName = "Honkytonk University" };
                artist1.Albums.Add(album);
                artist2.Albums.Add(album);
                //可用可无
                //context.Albums.Add(album);
                context.Artists.Add(artist1);
                context.Artists.Add(artist2);
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                Console.WriteLine("Artists and their albums...");

                var artists = context.Artists;
                foreach (var artist in artists)
                {
                    Console.WriteLine("{0} {1}", artist.FirstName, artist.LastName);
                    foreach (var album in artist.Albums)
                    {
                        Console.WriteLine("\t{0}", album.AlbumName);
                    }
                }
                Console.WriteLine("\nAlbums and their artists...");
                var albums = context.Albums;
                foreach (var album in albums)
                {
                    Console.WriteLine("{0}", album.AlbumName);
                    foreach (var artist in album.Artists)
                    {
                        Console.WriteLine("\t{0} {1}", artist.FirstName, artist.LastName);
                    }
                }
            }
        }
    }
}

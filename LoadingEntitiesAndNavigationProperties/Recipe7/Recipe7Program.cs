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

namespace LoadingEntitiesAndNavigationProperties.Recipe7
{
    /// <summary>
    /// 5-7  在别的LINQ查询操作中使用Include()方法
    /// </summary>
    public class Recipe7Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var club = new Club { Name = "Star City Chess Club", City = "New York" };
                club.Events.Add(new Event
                {
                    EventName = "Mid Cities Tournament",
                    EventDate = DateTime.Parse("1/09/2010"),
                    Club = club
                });
                club.Events.Add(new Event
                {
                    EventName = "State Finals Tournament",
                    EventDate = DateTime.Parse("2/12/2010"),
                    Club = club
                });
                club.Events.Add(new Event
                {
                    EventName = "Winter Classic",
                    EventDate = DateTime.Parse("12/18/2009"),
                    Club = club
                });

                context.Clubs.Add(club);

                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                var events = from ev in context.Events
                             where ev.Club.City == "New York"
                             group ev by ev.Club
                             into g
                             select g.FirstOrDefault(e1 => e1.EventDate == g.Min(evt => evt.EventDate));

                var eventWithClub = events.Include("Club").First();

                Console.WriteLine("The next New York club event is:");
                Console.WriteLine("\tEvent: {0}", eventWithClub.EventName);
                Console.WriteLine("\tDate: {0}", eventWithClub.EventDate.ToShortDateString());
                Console.WriteLine("\tClub: {0}", eventWithClub.Club.Name);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}

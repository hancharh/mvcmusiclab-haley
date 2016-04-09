namespace Music.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Music.Models.MusicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Music.Models.MusicContext";
        }

        protected override void Seed(Music.Models.MusicContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            context.Genres.AddOrUpdate(
                g => g.Name,
                new Genre {GenreID=1, Name = "Alternative" },
                new Genre {GenreID=2, Name = "Rock" },
                new Genre {GenreID=3, Name = "Indie"},
                new Genre {GenreID=4, Name = "Punk"},
                new Genre {GenreID=5, Name = "Hard Rock" },
                new Genre {GenreID=6, Name = "Pop" },
                new Genre {GenreID=7, Name = "Classic Rock" },
                new Genre {GenreID=8, Name = "Instrumental" },
                new Genre {GenreID=9, Name = "Electronic" },
                new Genre {GenreID=10, Name = "Pop Punk" }
                );

            context.Artists.AddOrUpdate(
                a => a.Name,
               
                new Artist { Name = "AC/DC" },
                new Artist { Name = "Aerosmith" },
                new Artist { Name = "Arctic Monkeys" },
                new Artist { Name = "The Black Keys" },
                new Artist { Name = "Cage the Elephant" },
                new Artist { Name = "Coldplay" },
                new Artist { Name = "David Bowie" },
                new Artist { Name = "A Day to Remember" },
                new Artist { Name = "Ed Sheeran" },
                new Artist { Name = "Explosions in the Sky" },
                new Artist { Name = "Fall Out Boy" },
                new Artist { Name = "The Fratellis" },
                new Artist { Name = "The Front Bottoms" },
                new Artist { Name = "The Glitch Mob" },
                new Artist { Name = "Gorillaz" },
                new Artist { Name = "Green Day" },
                new Artist { Name = "Led Zeppelin" },
                new Artist { Name = "Man Overboard" },
                new Artist { Name = "Modest Mouse" },
                new Artist { Name = "Ok Go" },
                new Artist { Name = "Radiohead" },
                new Artist { Name = "Red Hot Chili Peppers" },
                new Artist { Name = "Reel Big Fish" },
                new Artist { Name = "Say Anything" },
                new Artist { Name = "Senses Fail" },
                new Artist { Name = "The Shins" },
                new Artist { Name = "Smashing Pumpkins" },
                new Artist { Name = "Vitamin String Quartet" },
                new Artist { Name = "The White Stripes" },
                new Artist { Name = "The Who" },
                new Artist { Name = "Wolfmother" }
                );
        }
    }
}

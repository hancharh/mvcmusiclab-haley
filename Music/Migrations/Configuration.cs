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
                new Genre {GenreID=4, Name = "Punk"}
                );

            context.Artists.AddOrUpdate(
                a => a.Name,
                new Artist { Name = "The White Stripes" },
                new Artist { Name = "AC/DC" },
                new Artist { Name = "The Who" },
                new Artist { Name = "Modest Mouse" },
                new Artist { Name = "Led Zeppelin" },
                new Artist { Name = "The Shins" },
                new Artist { Name = "Green Day"}
                );
        }
    }
}

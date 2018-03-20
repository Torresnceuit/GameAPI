namespace PlayersAPI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PlayersAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlayersAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PlayersAPI.Models.ApplicationDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                userManager.Create(new ApplicationUser { UserName = "abcd@gmail.com", Email = "abcd@gmail.com" }, "Passc0de");

                context.SaveChanges();
            }

            if (context.Players.Count() == 0)
            {
                context.Players.Add(new Models.Player
                {
                    Age = 31,
                    Name = "Messi",
                    Position = "CF",
                    Nationality = "Argentinian",
                    Number = 10,
                    Avatar = "http://localhost:55903/Content/Upload/download.jpg",
                    TeamId = "90f9e7ae-224d-41e3-88a2-e712ea051e76"

                });

                context.SaveChanges();
            }

            if (context.Leagues.Count() == 0)
            {
                context.Leagues.Add(new League
                {
                    Id = "e942dcb3-cce7-486c-99ea-6bcaf9d88d47",
                    Name = "England",

                    Logo = "http://localhost:55903/Content/Upload/England.png"

                });
                context.SaveChanges();
            }

            if (context.Tournaments.Count() == 0)
            {
                context.Tournaments.Add(new Tournament
                {
                    Id = "e942dcb3-cce7-486c-99ea-6bcaf9d88d49",
                    Name = "English Premier League",
                    NoOfTeam = 20,
                    IsDone = false,
                    Logo = "http://localhost:55903/Content/Upload/EPL.png",
                    LeagueId = "e942dcb3-cce7-486c-99ea-6bcaf9d88d47"
                });
                context.SaveChanges();
            }

            if (context.Teams.Count() == 0)
            {
                context.Teams.Add(new Team
                {
                    Name = "Chelsea FC",
                    TourId = "e942dcb3-cce7-486c-99ea-6bcaf9d88d49",
                    Logo = "http://localhost:55903/Content/Upload/Chelsea.png"
                });
                context.SaveChanges();
            }

            if (context.Rounds.Count() == 0)
            {
                context.Rounds.Add(new Round
                {

                });
            }


        }
    }
}

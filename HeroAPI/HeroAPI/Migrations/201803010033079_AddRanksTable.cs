namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRanksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TourId = c.String(maxLength: 128),
                        TeamId = c.String(maxLength: 128),
                        Games = c.Int(nullable: false),
                        Won = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        Draw = c.Int(nullable: false),
                        Goals = c.Int(nullable: false),
                        Concede = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Tournaments", t => t.TourId)
                .Index(t => t.TourId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ranks", "TourId", "dbo.Tournaments");
            DropForeignKey("dbo.Ranks", "TeamId", "dbo.Teams");
            DropIndex("dbo.Ranks", new[] { "TeamId" });
            DropIndex("dbo.Ranks", new[] { "TourId" });
            DropTable("dbo.Ranks");
        }
    }
}

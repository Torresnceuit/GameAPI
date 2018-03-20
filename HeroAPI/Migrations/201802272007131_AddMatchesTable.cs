namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HomeScore = c.Int(nullable: false),
                        AwayScore = c.Int(nullable: false),
                        HomeId = c.String(maxLength: 128),
                        AwayId = c.String(maxLength: 128),
                        RoundId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayId)
                .ForeignKey("dbo.Teams", t => t.HomeId)
                .ForeignKey("dbo.Rounds", t => t.RoundId)
                .Index(t => t.HomeId)
                .Index(t => t.AwayId)
                .Index(t => t.RoundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.Matches", "HomeId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayId", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "RoundId" });
            DropIndex("dbo.Matches", new[] { "AwayId" });
            DropIndex("dbo.Matches", new[] { "HomeId" });
            DropTable("dbo.Matches");
        }
    }
}

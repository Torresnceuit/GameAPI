namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        NoOfTeam = c.Int(nullable: false),
                        Logo = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        LeagueId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
                .Index(t => t.LeagueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "LeagueId", "dbo.Leagues");
            DropIndex("dbo.Tournaments", new[] { "LeagueId" });
            DropTable("dbo.Tournaments");
        }
    }
}

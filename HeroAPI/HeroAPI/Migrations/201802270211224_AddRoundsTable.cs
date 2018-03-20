namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoundsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        TourId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TourId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rounds", "TourId", "dbo.Tournaments");
            DropIndex("dbo.Rounds", new[] { "TourId" });
            DropTable("dbo.Rounds");
        }
    }
}

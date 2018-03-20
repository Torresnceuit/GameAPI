namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Logo = c.String(),
                        TourId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TourId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "TourId", "dbo.Tournaments");
            DropIndex("dbo.Teams", new[] { "TourId" });
            DropTable("dbo.Teams");
        }
    }
}

namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMatchesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "TourId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Matches", "TourId");
            AddForeignKey("dbo.Matches", "TourId", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "TourId", "dbo.Tournaments");
            DropIndex("dbo.Matches", new[] { "TourId" });
            DropColumn("dbo.Matches", "TourId");
        }
    }
}

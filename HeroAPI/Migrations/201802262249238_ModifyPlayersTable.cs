namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPlayersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Players", new[] { "UserId" });
            AddColumn("dbo.Players", "TeamId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Players", "TeamId");
            AddForeignKey("dbo.Players", "TeamId", "dbo.Teams", "Id");
            DropColumn("dbo.Players", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropColumn("dbo.Players", "TeamId");
            CreateIndex("dbo.Players", "UserId");
            AddForeignKey("dbo.Players", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

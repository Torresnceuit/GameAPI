namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Position = c.String(),
                        Nationality = c.String(),
                        Number = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Players", new[] { "UserId" });
            DropTable("dbo.Players");
        }
    }
}

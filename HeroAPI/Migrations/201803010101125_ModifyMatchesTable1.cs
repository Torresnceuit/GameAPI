namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMatchesTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "IsDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "IsDone");
        }
    }
}

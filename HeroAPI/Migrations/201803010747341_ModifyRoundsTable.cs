namespace PlayersAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRoundsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rounds", "Name", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rounds", "Name", c => c.String());
        }
    }
}

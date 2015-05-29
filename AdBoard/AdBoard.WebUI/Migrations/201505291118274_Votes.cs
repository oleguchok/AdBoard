namespace AdBoard.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Votes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Votes");
        }
    }
}

namespace AdBoard.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoritesAds : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "DateOfBirthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DateOfBirthday", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
    }
}

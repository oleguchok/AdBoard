namespace AdBoard.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFav : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FavoritesAds", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FavoritesAds");
        }
    }
}

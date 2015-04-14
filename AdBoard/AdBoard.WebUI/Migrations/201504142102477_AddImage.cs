namespace AdBoard.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageData", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageMimeType");
            DropColumn("dbo.AspNetUsers", "ImageData");
        }
    }
}

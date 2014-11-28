namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countphotos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "CountPhotos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "CountPhotos");
        }
    }
}

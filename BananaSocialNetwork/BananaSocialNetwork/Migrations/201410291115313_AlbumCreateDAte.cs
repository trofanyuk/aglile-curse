namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumCreateDAte : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "DateCreate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "DateCreate");
        }
    }
}

namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "IdContent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "IdContent");
        }
    }
}

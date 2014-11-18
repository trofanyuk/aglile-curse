namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friend2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "confirm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "confirm");
        }
    }
}

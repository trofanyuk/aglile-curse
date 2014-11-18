namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscribers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        subscriber_Id = c.String(maxLength: 128),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.subscriber_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.subscriber_Id)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscribers", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscribers", "subscriber_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Subscribers", new[] { "user_Id" });
            DropIndex("dbo.Subscribers", new[] { "subscriber_Id" });
            DropTable("dbo.Subscribers");
        }
    }
}

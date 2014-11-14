namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        friend_Id = c.String(maxLength: 128),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.friend_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.friend_Id)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "friend_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "user_Id" });
            DropIndex("dbo.Friends", new[] { "friend_Id" });
            DropTable("dbo.Friends");
        }
    }
}

namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Album4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropTable("dbo.Albums");
        }
    }
}

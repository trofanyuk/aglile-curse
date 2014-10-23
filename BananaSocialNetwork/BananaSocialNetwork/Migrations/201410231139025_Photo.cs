namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhotoPath = c.String(),
                        Adress = c.String(),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Photos", new[] { "Album_Id" });
            DropTable("dbo.Photos");
        }
    }
}

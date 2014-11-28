namespace BananaSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Adress", c => c.String());
            AddColumn("dbo.AspNetUsers", "AvatatPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvatatPath");
            DropColumn("dbo.AspNetUsers", "Adress");
            DropColumn("dbo.AspNetUsers", "Sex");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
        }
    }
}

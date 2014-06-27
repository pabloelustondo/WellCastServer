namespace WellCastServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conditions", "Validated", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Profiles", "Gender", c => c.String());
            DropColumn("dbo.Users", "x");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "x", c => c.Int(nullable: false));
            AlterColumn("dbo.Profiles", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Conditions", "Validated");
        }
    }
}

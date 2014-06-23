namespace WellCastServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Symptoms", "KeyName", c => c.String());
            AddColumn("dbo.Conditions", "KeyName", c => c.String());
            AddColumn("dbo.SymptomCategories", "KeyName", c => c.String());
            AddColumn("dbo.Locations", "KeyName", c => c.String());
            AddColumn("dbo.Users", "KeyName", c => c.String());
            AddColumn("dbo.Profiles", "KeyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "KeyName");
            DropColumn("dbo.Users", "KeyName");
            DropColumn("dbo.Locations", "KeyName");
            DropColumn("dbo.SymptomCategories", "KeyName");
            DropColumn("dbo.Conditions", "KeyName");
            DropColumn("dbo.Symptoms", "KeyName");
        }
    }
}

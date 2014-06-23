namespace WellCastServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SymptomCategoryID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SymptomCategories", t => t.SymptomCategoryID, cascadeDelete: true)
                .Index(t => t.SymptomCategoryID);
            
            CreateTable(
                "dbo.ConditionSymptoms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ConditionID = c.Guid(nullable: false),
                        SymptomID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID, cascadeDelete: true)
                .ForeignKey("dbo.Symptoms", t => t.SymptomID, cascadeDelete: true)
                .Index(t => t.ConditionID)
                .Index(t => t.SymptomID);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SymptomCategories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WellCastLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Message = c.String(),
                        timeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        lat = c.Double(nullable: false),
                        lon = c.Double(nullable: false),
                        x = c.Int(nullable: false),
                        y = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ConditionProfiles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ConditionID = c.Guid(nullable: false),
                        ProfileID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileID, cascadeDelete: true)
                .Index(t => t.ConditionID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.ConditionForecasts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProfileID = c.Guid(),
                        LocationID = c.Guid(),
                        ConditionID = c.Guid(),
                        Date = c.DateTime(nullable: false),
                        RiskDay0 = c.Int(nullable: false),
                        RiskDay1 = c.Int(nullable: false),
                        RiskDay2 = c.Int(nullable: false),
                        RiskDay3 = c.Int(nullable: false),
                        RiskDay4 = c.Int(nullable: false),
                        RiskDay5 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID)
                .Index(t => t.ProfileID)
                .Index(t => t.LocationID)
                .Index(t => t.ConditionID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ConditionForecasts", new[] { "ConditionID" });
            DropIndex("dbo.ConditionForecasts", new[] { "LocationID" });
            DropIndex("dbo.ConditionForecasts", new[] { "ProfileID" });
            DropIndex("dbo.ConditionProfiles", new[] { "ProfileID" });
            DropIndex("dbo.ConditionProfiles", new[] { "ConditionID" });
            DropIndex("dbo.Profiles", new[] { "UserID" });
            DropIndex("dbo.Locations", new[] { "UserID" });
            DropIndex("dbo.ConditionSymptoms", new[] { "SymptomID" });
            DropIndex("dbo.ConditionSymptoms", new[] { "ConditionID" });
            DropIndex("dbo.Symptoms", new[] { "SymptomCategoryID" });
            DropForeignKey("dbo.ConditionForecasts", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.ConditionForecasts", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ConditionForecasts", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ConditionProfiles", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ConditionProfiles", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Profiles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Locations", "UserID", "dbo.Users");
            DropForeignKey("dbo.ConditionSymptoms", "SymptomID", "dbo.Symptoms");
            DropForeignKey("dbo.ConditionSymptoms", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Symptoms", "SymptomCategoryID", "dbo.SymptomCategories");
            DropTable("dbo.ConditionForecasts");
            DropTable("dbo.ConditionProfiles");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.Locations");
            DropTable("dbo.WellCastLogs");
            DropTable("dbo.SymptomCategories");
            DropTable("dbo.Conditions");
            DropTable("dbo.ConditionSymptoms");
            DropTable("dbo.Symptoms");
        }
    }
}

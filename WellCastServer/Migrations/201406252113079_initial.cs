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
                        ID = c.String(nullable: false, maxLength: 128),
                        SymptomCategoryID = c.String(maxLength: 128),
                        KeyName = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SymptomCategories", t => t.SymptomCategoryID)
                .Index(t => t.SymptomCategoryID);
            
            CreateTable(
                "dbo.ConditionSymptoms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ConditionID = c.String(maxLength: 128),
                        SymptomID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID)
                .ForeignKey("dbo.Symptoms", t => t.SymptomID)
                .Index(t => t.ConditionID)
                .Index(t => t.SymptomID);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        KeyName = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SymptomCategories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        KeyName = c.String(),
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
                        Message2 = c.String(),
                        timeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        lat = c.Double(nullable: false),
                        lon = c.Double(nullable: false),
                        x = c.Int(nullable: false),
                        y = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        x = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ConditionProfiles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ConditionID = c.String(maxLength: 128),
                        ProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ConditionID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.ConditionForecasts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ForecastID = c.Guid(nullable: false),
                        ProfileID = c.String(maxLength: 128),
                        LocationID = c.String(maxLength: 128),
                        ConditionID = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        RiskDay0 = c.Int(nullable: false),
                        RiskDay1 = c.Int(nullable: false),
                        RiskDay2 = c.Int(nullable: false),
                        RiskDay3 = c.Int(nullable: false),
                        RiskDay4 = c.Int(nullable: false),
                        RiskDay5 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Forecasts", t => t.ForecastID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.Conditions", t => t.ConditionID)
                .Index(t => t.ForecastID)
                .Index(t => t.ProfileID)
                .Index(t => t.LocationID)
                .Index(t => t.ConditionID);
            
            CreateTable(
                "dbo.Forecasts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProfileID = c.String(maxLength: 128),
                        LocationID = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        RiskDay0 = c.Int(nullable: false),
                        RiskDay1 = c.Int(nullable: false),
                        RiskDay2 = c.Int(nullable: false),
                        RiskDay3 = c.Int(nullable: false),
                        RiskDay4 = c.Int(nullable: false),
                        RiskDay5 = c.Int(nullable: false),
                        ReportDay0 = c.String(),
                        ReportDay1 = c.String(),
                        ReportDay2 = c.String(),
                        ReportDay3 = c.String(),
                        ReportDay4 = c.String(),
                        ReportDay5 = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .Index(t => t.ProfileID)
                .Index(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Forecasts", new[] { "LocationID" });
            DropIndex("dbo.Forecasts", new[] { "ProfileID" });
            DropIndex("dbo.ConditionForecasts", new[] { "ConditionID" });
            DropIndex("dbo.ConditionForecasts", new[] { "LocationID" });
            DropIndex("dbo.ConditionForecasts", new[] { "ProfileID" });
            DropIndex("dbo.ConditionForecasts", new[] { "ForecastID" });
            DropIndex("dbo.ConditionProfiles", new[] { "ProfileID" });
            DropIndex("dbo.ConditionProfiles", new[] { "ConditionID" });
            DropIndex("dbo.Profiles", new[] { "UserID" });
            DropIndex("dbo.Locations", new[] { "UserID" });
            DropIndex("dbo.ConditionSymptoms", new[] { "SymptomID" });
            DropIndex("dbo.ConditionSymptoms", new[] { "ConditionID" });
            DropIndex("dbo.Symptoms", new[] { "SymptomCategoryID" });
            DropForeignKey("dbo.Forecasts", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Forecasts", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ConditionForecasts", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.ConditionForecasts", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ConditionForecasts", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ConditionForecasts", "ForecastID", "dbo.Forecasts");
            DropForeignKey("dbo.ConditionProfiles", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ConditionProfiles", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Profiles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Locations", "UserID", "dbo.Users");
            DropForeignKey("dbo.ConditionSymptoms", "SymptomID", "dbo.Symptoms");
            DropForeignKey("dbo.ConditionSymptoms", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Symptoms", "SymptomCategoryID", "dbo.SymptomCategories");
            DropTable("dbo.Forecasts");
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

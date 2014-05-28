﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<WellCastServerContext>
    {
        protected override void Seed(WellCastServerContext db)
    {
        base.Seed(db);

        var condition1 = new Condition { Name = "Condition1", Description = "Condition1" };
        var condition2 = new Condition { Name = "Condition2", Description = "Condition2" };
        db.WellCastConditions.Add(condition1);
        db.WellCastConditions.Add(condition2);
        db.SaveChanges();

        var symptomCategory1 = new SymptomCategory { Name = "SymptomCategory1", Description = "SymptomCategory1" };
        var symptomCategory2 = new SymptomCategory { Name = "SymptomCategory2", Description = "SymptomCategory2" };

        db.WellCastSymptomCategories.Add(symptomCategory1);
        db.WellCastSymptomCategories.Add(symptomCategory2);
        db.SaveChanges();

        var symptom1 = new Symptom { Name = "Symptom1", Description = "Symptom1", SymptomCategoryID = symptomCategory1.ID };
        var symptom2 = new Symptom { Name = "Symptom2", Description = "Symptom2", SymptomCategoryID = symptomCategory1.ID };
        var symptom3 = new Symptom { Name = "Symptom3", Description = "Symptom3", SymptomCategoryID = symptomCategory2.ID };
        var symptom4 = new Symptom { Name = "Symptom4", Description = "Symptom4", SymptomCategoryID = symptomCategory2.ID };
     
        db.WellCastSymptoms.Add(symptom1);
        db.WellCastSymptoms.Add(symptom2);
        db.WellCastSymptoms.Add(symptom3);
        db.WellCastSymptoms.Add(symptom4);
        db.SaveChanges();

        var conditionSymptom1 = new ConditionSymptom { ConditionID = condition1.ID, SymptomID = symptom1.ID };
        var conditionSymptom2 = new ConditionSymptom { ConditionID = condition1.ID, SymptomID = symptom2.ID };
        var conditionSymptom3 = new ConditionSymptom { ConditionID = condition2.ID, SymptomID = symptom3.ID };
        var conditionSymptom4 = new ConditionSymptom { ConditionID = condition2.ID, SymptomID = symptom4.ID };
        var conditionSymptom5 = new ConditionSymptom { ConditionID = condition2.ID, SymptomID = symptom1.ID };
        var conditionSymptom6 = new ConditionSymptom { ConditionID = condition1.ID, SymptomID = symptom3.ID };

        db.WellCastConditionSymptoms.Add(conditionSymptom1);
        db.WellCastConditionSymptoms.Add(conditionSymptom2);
        db.WellCastConditionSymptoms.Add(conditionSymptom3);
        db.WellCastConditionSymptoms.Add(conditionSymptom4);
        db.WellCastConditionSymptoms.Add(conditionSymptom5);
        db.WellCastConditionSymptoms.Add(conditionSymptom6);
        db.SaveChanges();


        var location1 = new Location { Name = "Location1", Description = "Location1" };
        var location2 = new Location { Name = "Location2", Description = "Location2" };

        location1.Name = "CN Tower";
        location1.Description = "CN Tower 1 Front St W";
        location1.lat = 43.642811;
        location1.lon = -79.387046;
        location1.y = 133;
        location1.x = 241;


        location2.Name = "Vancouver";
        location2.Description = "Queen Elizabeth Park 4600 Cambie St, Vancouver, BC V5Y 2M9";
        location2.lat = 49.24073;
        location2.lon = -123.113261;
        location2.y = 143;
        location2.x = 138;


        db.WellCastLocations.Add(location1);
        db.WellCastLocations.Add(location2);
        db.SaveChanges();



    }
    }
}
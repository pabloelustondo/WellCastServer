﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
  //  public class DatabaseContextInitializer : DropCreateDatabaseAlways<WellCastServerContext>
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<WellCastServerContext>
    {
        protected override void Seed(WellCastServerContext db)
    {
        base.Seed(db);

        var condition1 = new Condition { Name = "Condition1", Description = "Condition1" };
        var condition2 = new Condition { Name = "Condition2", Description = "Condition2" };
        condition1.KeyName = condition1.Name;
        condition2.KeyName = condition2.Name;
        condition1.ID = condition1.Name;
        condition2.ID = condition2.Name;

        db.WellCastConditions.Add(condition1);
        db.WellCastConditions.Add(condition2);
        db.SaveChanges();

        var symptomCategory1 = new SymptomCategory { Name = "SymptomCategory1", Description = "SymptomCategory1" };
        var symptomCategory2 = new SymptomCategory { Name = "SymptomCategory2", Description = "SymptomCategory2" };
        symptomCategory1.KeyName = symptomCategory1.Name;
        symptomCategory2.KeyName = symptomCategory2.Name;
        symptomCategory1.ID = symptomCategory1.Name;
        symptomCategory2.ID = symptomCategory2.Name;

        db.WellCastSymptomCategories.Add(symptomCategory1);
        db.WellCastSymptomCategories.Add(symptomCategory2);
        db.SaveChanges();

        var symptom1 = new Symptom { Name = "Symptom1", Description = "Symptom1", SymptomCategoryID = symptomCategory1.ID };
        var symptom2 = new Symptom { Name = "Symptom2", Description = "Symptom2", SymptomCategoryID = symptomCategory1.ID };
        var symptom3 = new Symptom { Name = "Symptom3", Description = "Symptom3", SymptomCategoryID = symptomCategory2.ID };
        var symptom4 = new Symptom { Name = "Symptom4", Description = "Symptom4", SymptomCategoryID = symptomCategory2.ID };

        symptom1.KeyName = symptom1.Name;
        symptom2.KeyName = symptom2.Name;
        symptom3.KeyName = symptom3.Name;
        symptom4.KeyName = symptom4.Name;

        symptom1.ID = symptom1.Name;
        symptom2.ID = symptom2.Name;
        symptom3.ID = symptom3.Name;
        symptom4.ID = symptom4.Name;
     
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



        var user1 = new User { Name = "User1"};
        var user2 = new User { Name = "User2"};

        user1.ID = user1.Name;
        user2.ID = user2.Name;

        db.WellCastUsers.Add(user1);
        db.WellCastUsers.Add(user2);

        db.SaveChanges();

        var location1 = new Location { Name = "Location1", Description = "Location1" };
        var location2 = new Location { Name = "Location2", Description = "Location2" };
        var location3 = new Location { Name = "Location3", Description = "Location3" };
   
        location1.ID = location1.Name;
        location2.ID = location2.Name;
        location3.ID = location3.Name;


        location1.UserID = user1.ID;
        location1.Name = "CN Tower";
        location1.Description = "CN Tower 1 Front St W";
        location1.lat = 43.642811;
        location1.lon = -79.387046;
        location1.y = 133;
        location1.x = 241;


        location2.UserID = user2.ID;
        location2.Name = "Vancouver";
        location2.Description = "Queen Elizabeth Park 4600 Cambie St, Vancouver, BC V5Y 2M9";
        location2.lat = 49.24073;
        location2.lon = -123.113261;
        location2.y = 143;
        location2.x = 138;

        location3.UserID = user1.ID;
        location3.Name = "Vancouver2";
        location3.Description = "Queen Elizabeth Park 4600 Cambie St, Vancouver, BC V5Y 2M9";
        location3.lat = 49.24073;
        location3.lon = -123.113261;
        location3.y = 143;
        location3.x = 138;


        db.WellCastLocations.Add(location1);
        db.WellCastLocations.Add(location2);
        db.SaveChanges();

        var profile1 = new Profile { UserID = user1.ID, Name = "Profile1", Description = "Profile1", Gender = WellCastGender.Male, Age = 46 };
        var profile2 = new Profile { UserID = user1.ID, Name = "Profile2", Description = "Profile2", Gender = WellCastGender.Male, Age = 35 };
        var profile3 = new Profile { UserID = user2.ID, Name = "Profile3", Description = "Profile3", Gender = WellCastGender.Female, Age = 23 };
        var profile4 = new Profile { UserID = user2.ID, Name = "Profile4", Description = "Profile4", Gender = WellCastGender.Female, Age = 18 };

        profile1.ID = profile1.Name;
        profile2.ID = profile2.Name;
        profile3.ID = profile3.Name;
        profile4.ID = profile4.Name;

        db.WellCastProfiles.Add(profile1);
        db.WellCastProfiles.Add(profile2);
        db.WellCastProfiles.Add(profile3);
        db.WellCastProfiles.Add(profile4);
        db.SaveChanges();


        var conditionProfile1 = new ConditionProfile { ConditionID = condition1.ID, ProfileID = profile1.ID };
        var conditionProfile2 = new ConditionProfile { ConditionID = condition1.ID, ProfileID = profile2.ID };
        var conditionProfile3 = new ConditionProfile { ConditionID = condition2.ID, ProfileID = profile3.ID };
        var conditionProfile4 = new ConditionProfile { ConditionID = condition2.ID, ProfileID = profile4.ID };
        var conditionProfile5 = new ConditionProfile { ConditionID = condition2.ID, ProfileID = profile1.ID };
        var conditionProfile6 = new ConditionProfile { ConditionID = condition1.ID, ProfileID = profile3.ID };

        db.WellCastConditionProfiles.Add(conditionProfile1);
        db.WellCastConditionProfiles.Add(conditionProfile2);
        db.WellCastConditionProfiles.Add(conditionProfile3);
        db.WellCastConditionProfiles.Add(conditionProfile4);
        db.WellCastConditionProfiles.Add(conditionProfile5);
        db.WellCastConditionProfiles.Add(conditionProfile6);
        db.SaveChanges();


    }
    }
}
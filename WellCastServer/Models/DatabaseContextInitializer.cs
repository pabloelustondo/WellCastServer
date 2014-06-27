using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
  //public class DatabaseContextInitializer : DropCreateDatabaseAlways<WellCastServerContext>
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



     


    }
    }
}
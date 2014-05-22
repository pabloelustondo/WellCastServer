using System;
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
        for (int c = 0; c < 2; c++) {
            db.WellCastConditions.Add(new Condition { ID = c, Name = "Condition" + c, Description = "Condition" + c });
        }
        for (int c = 0; c < 2; c++)
        {
            db.WellCastSymptomCategories.Add(new SymptomCategory { ID = c, Name = "SymptomCategory" + c, Description = "SymptomCategory" + c });
        }
        for (int c = 0; c < 4; c++)
        {
            int cc = c / 2;
            db.WellCastSymptoms.Add(new Symptom { ID = c, Name = "Symptom" + c, Description = "Symptom" + c, SymptomCategoryID = cc });
        }

        db.WellCastConditionSymptoms.Add(new ConditionSymptom { ID = 0, ConditionID = 0, SymptomID = 0 });
        db.WellCastConditionSymptoms.Add(new ConditionSymptom { ID = 1, ConditionID = 0, SymptomID = 1 });
        db.WellCastConditionSymptoms.Add(new ConditionSymptom { ID = 2, ConditionID = 1, SymptomID = 2 });
        db.WellCastConditionSymptoms.Add(new ConditionSymptom { ID = 3, ConditionID = 1, SymptomID = 3 });

     
              

    }
    }
}
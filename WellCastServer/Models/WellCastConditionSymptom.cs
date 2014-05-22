using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionSymptom
    {
            public int ID { get; set; }

            public int ConditionID { get; set; }
            public virtual Condition Condition { get; set; }

            public int SymptomID { get; set; }
            public virtual Symptom Symptom { get; set; }

    }
}
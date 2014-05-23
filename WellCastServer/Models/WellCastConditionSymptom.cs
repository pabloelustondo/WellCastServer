using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionSymptom: WellCastRelation
    {
            public Guid ConditionID { get; set; }
            public virtual Condition Condition { get; set; }

            public Guid SymptomID { get; set; }
            public virtual Symptom Symptom { get; set; }

    }
}
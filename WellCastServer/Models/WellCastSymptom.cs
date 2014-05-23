using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WellCastServer.Models
{

    public class Symptom: WellCastEntity
    {
        public virtual List<Guid> ConditionIDs
        {
            get
            {
                var _ConditionIDs = new List<Guid>();
                if (ConditionSymptoms == null) return _ConditionIDs;
                foreach (var cs in ConditionSymptoms)
                {
                    _ConditionIDs.Add(cs.Condition.ID);
                }
                return _ConditionIDs;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionSymptom> ConditionSymptoms { get; set; }

        public Guid SymptomCategoryID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual SymptomCategory SymptomCategory { get; set; }
    }
}
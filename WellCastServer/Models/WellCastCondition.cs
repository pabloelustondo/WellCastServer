using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Condition: WellCastEntity
    {
        public virtual List<Guid> SymptomIDs
        {
            get
            {
                var _SymptomIDs = new List<Guid>();
                if (ConditionSymptoms == null) return _SymptomIDs;
                foreach (var cs in ConditionSymptoms)
                {
                    _SymptomIDs.Add(cs.Symptom.ID);
                }
                return _SymptomIDs;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionSymptom> ConditionSymptoms { get; set; }
      }
}
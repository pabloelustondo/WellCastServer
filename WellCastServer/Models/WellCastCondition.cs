using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Condition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<int> SymptomIDs
        {
            get
            {
                var _SymptomIDs = new List<int>();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Condition: WellCastEntityWithKeyName
    {
        public bool Validated { get; set; }
        public virtual List<String> SymptomKeys
        {
            get
            {
                var _SymptomKeys = new List<String>();
                if (ConditionSymptoms == null) return _SymptomKeys;
                foreach (var cs in ConditionSymptoms)
                {
                    _SymptomKeys.Add(cs.Symptom.KeyName);
                }
                return _SymptomKeys;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionSymptom> ConditionSymptoms { get; set; }
      }
}
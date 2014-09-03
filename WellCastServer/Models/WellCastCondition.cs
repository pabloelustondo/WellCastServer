using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Condition: WellCastEntityWithKeyName
    {
       [Newtonsoft.Json.JsonIgnore]
        public bool Validated { get; set; }

       public string condition_id { get { return KeyName; } }

        public virtual List<String> symptoms
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
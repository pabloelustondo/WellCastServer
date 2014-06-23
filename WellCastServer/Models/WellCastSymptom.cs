using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WellCastServer.Models
{

    public class Symptom: WellCastEntityWithKeyName
    {
        public virtual List<String> ConditionKeys
        {
            get
            {
                var _ConditionKeys = new List<String>();
                if (ConditionSymptoms == null) return _ConditionKeys;
                foreach (var cs in ConditionSymptoms)
                {
                    _ConditionKeys.Add(cs.Condition.KeyName);
                }
                return _ConditionKeys;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionSymptom> ConditionSymptoms { get; set; }

        public Guid SymptomCategoryID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual SymptomCategory SymptomCategory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WellCastServer.Models
{

    public class Symptom: WellCastEntityWithKeyName
    {
        /*
         *          "name":"Trouble Breathing",
         "symptom_category":"Breathing",
         "_id":"5366faac82f752bd059b6606",
         "conditions":[  
            "copd",
            "asthma"
         ],
         */
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

        public string SymptomCategoryID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual SymptomCategory SymptomCategory { get; set; }
    }
}
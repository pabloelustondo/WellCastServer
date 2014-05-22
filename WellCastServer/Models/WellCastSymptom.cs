using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WellCastServer.Models
{

    /*
     [{
	_id: 1,
	name: "Trouble Breathing", 
	symptom_category: "Breathing",
	conditions: [4,5]    
	# blank means a general symptom (included 
	in all conditions)
}]

     */
    public class Symptom
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<int> ConditionIDs
        {
            get
            {
                var _ConditionIDs = new List<int>();
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

        public int SymptomCategoryID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual SymptomCategory SymptomCategory { get; set; }
    }
}
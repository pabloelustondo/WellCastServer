using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public enum WellCastGender{
        Male,
        Female
    }
    public class Profile: WellCastEntity
    {
        /*
_id: 1,
name: “Nick”,
gender: “male”,
age: 44,
	conditions: [ 1, 5 ]
         */

        public WellCastGender Gender { get; set; }
        public DateTime DOB { get; set; }

        public virtual List<Guid> ConditionIDs
        {
            get
            {
                var _ConditionIDs = new List<Guid>();
                if (ConditionProfiles == null) return _ConditionIDs;
                foreach (var cs in ConditionProfiles)
                {
                    _ConditionIDs.Add(cs.Condition.ID);
                }
                return _ConditionIDs;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionProfile> ConditionProfiles { get; set; }

    }
}
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
        public String UserID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual User User { get; set; }

        public WellCastGender Gender { get; set; }
        public int Age { get; set; }

        public virtual List<String> ConditionIDs
        {
            get
            {
                var _ConditionIDs = new List<String>();
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
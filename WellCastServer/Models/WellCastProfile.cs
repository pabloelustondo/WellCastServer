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

        public String Gender { get; set; }
        public int Age { get; set; }

        public virtual List<String> ConditionIDs { get; set; }
        public virtual List<String> LocationIDs { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionProfile> ConditionProfiles { get; set; }

    }
}
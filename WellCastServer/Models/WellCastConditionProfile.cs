using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionProfile : WellCastRelation
    {
        public String ConditionID { get; set; }
        public virtual Condition Condition { get; set; }

        public String ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

    }
}
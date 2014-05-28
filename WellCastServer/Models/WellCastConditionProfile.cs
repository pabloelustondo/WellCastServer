using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionProfile : WellCastRelation
    {
        public Guid ConditionID { get; set; }
        public virtual Condition Condition { get; set; }

        public Guid ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

    }
}
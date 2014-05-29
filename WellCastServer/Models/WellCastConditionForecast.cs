using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionForecast: WellCastRelation
    {

        public Guid? ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

        public Guid? LocationID { get; set; }
        public virtual Location Location { get; set; }

        public Guid? ConditionID { get; set; }
        public virtual Condition Condition { get; set; }

        public DateTime Date { get; set; }

        public int RiskDay0 { get; set; }
        public int RiskDay1 { get; set; }
        public int RiskDay2 { get; set; }
        public int RiskDay3 { get; set; }
        public int RiskDay4 { get; set; }
        public int RiskDay5 { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Forecast: WellCastRelation
    {

        public Guid? ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

        public Guid? LocationID { get; set; }
        public virtual Location Location { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Location> ConditionForecasts { get; set; }

        public DateTime Date { get; set; }

        public int RiskDay0 { get; set; }
        public int RiskDay1 { get; set; }
        public int RiskDay2 { get; set; }
        public int RiskDay3 { get; set; }
        public int RiskDay4 { get; set; }
        public int RiskDay5 { get; set; }

        public int ReportDay0 { get; set; }
        public int ReportDay1 { get; set; }
        public int ReportDay2 { get; set; }
        public int ReportDay3 { get; set; }
        public int ReportDay4 { get; set; }
        public int ReportDay5 { get; set; }

    }
}
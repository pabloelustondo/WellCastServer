using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionForecast: WellCastRelation
    {
        public Guid ForecastID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Forecast Forecast { get; set; }

        public String ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

        public String LocationID { get; set; }
        public virtual Location Location { get; set; }

        public String ConditionID { get; set; }
        public virtual Condition Condition { get; set; }

        public DateTime Date { get; set; }

        public int RiskDay0 { get; set; }
        public int RiskDay1 { get; set; }
        public int RiskDay2 { get; set; }
        public int RiskDay3 { get; set; }
        public int RiskDay4 { get; set; }
        public int RiskDay5 { get; set; }

        [NotMapped]
        public virtual List<int> RiskDay
        {
            get
            {
                var riskDay = new List<int>();
                riskDay.Add(RiskDay0);
                riskDay.Add(RiskDay1);
                riskDay.Add(RiskDay2);
                riskDay.Add(RiskDay3);
                riskDay.Add(RiskDay4);
                riskDay.Add(RiskDay5);
                return riskDay;
            }
        } 

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Forecast: WellCastRelation
    {

        public Guid? ProfileID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Profile Profile { get; set; }

        public Guid? LocationID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Location Location { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ConditionForecast> ConditionForecasts { get; set; }

        public DateTime Date { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay0 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay1 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay2 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay3 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay4 { get; set; }       
               [Newtonsoft.Json.JsonIgnore]
        public int RiskDay5 { get; set; }


               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay0 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay1 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay2 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay3 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay4 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public int ReportDay5 { get; set; }

        [NotMapped]
        public virtual List<Report> Reports { 
            get{

                var reports = new List<Report>();
                var report0 = new Report();

                report0.Date = Date;
                report0.Risk = RiskDay0;
                report0.conditionReports = new List<ConditionReport>();
                foreach (var conditionForecast in ConditionForecasts)
                {
                    var conditionReport = new ConditionReport();
                    conditionReport.ConditionID = (Guid) conditionForecast.ConditionID;
                    conditionReport.Risk = conditionForecast.RiskDay0;
                    report0.conditionReports.Add(conditionReport);
                }

                reports.Add(report0);
                return reports;            
            }       
        }

    }
}
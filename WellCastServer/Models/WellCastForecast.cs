using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Forecast: WellCastRelation
    {

        public String ProfileID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Profile Profile { get; set; }

        public String LocationID { get; set; }
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
        public string ReportDay0 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public string ReportDay1 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public string ReportDay2 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public string ReportDay3 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public string ReportDay4 { get; set; }
               [Newtonsoft.Json.JsonIgnore]
        public string ReportDay5 { get; set; }

           [NotMapped]
           [Newtonsoft.Json.JsonIgnore]
        public virtual List<string> ReportDay { get{
            var reportDay = new List<string>();
            reportDay.Add(ReportDay0);
            reportDay.Add(ReportDay1);
            reportDay.Add(ReportDay2);
            reportDay.Add(ReportDay3);
            reportDay.Add(ReportDay4);
            reportDay.Add(ReportDay5);
            return reportDay;
        }}
           [NotMapped]
           [Newtonsoft.Json.JsonIgnore]
        public virtual List<int> RiskDay { get {
            var riskDay = new List<int>();
            riskDay.Add(RiskDay0);
            riskDay.Add(RiskDay1);
            riskDay.Add(RiskDay2);
            riskDay.Add(RiskDay3);
            riskDay.Add(RiskDay4);
            riskDay.Add(RiskDay5);
            return riskDay;
        } } 

        [NotMapped]
        public virtual List<Report> Reports { 
            get{

                var reports = new List<Report>();
                for (int d = 0; d < 6; d++) { 
                var report = new Report();

                report.Date = Date.AddDays(d);
                report.Risk = RiskDay[d];
                report.ReportNote = ReportDay[d];
                report.conditionReports = new List<ConditionReport>();
                foreach (var conditionForecast in ConditionForecasts)
                {
                    var conditionReport = new ConditionReport();
                    conditionReport.ConditionKey = conditionForecast.Condition.KeyName;
                    conditionReport.Risk = conditionForecast.RiskDay[d];
                    report.conditionReports.Add(conditionReport);
                }

                reports.Add(report);
                }
                return reports;   
            }
         
            }       
        }

    }
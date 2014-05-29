using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Report
    {
        [Newtonsoft.Json.JsonIgnore]
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public int Risk { get; set; }
        public int ReportNote { get; set; }

        public List<ConditionReport> conditionReports { get; set; }
    }
}
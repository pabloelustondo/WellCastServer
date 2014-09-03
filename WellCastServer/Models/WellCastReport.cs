using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Report
    {
        public int _id { get; set; }

        public DateTime date { get; set; }
        public int risk { get; set; }
        public string report { get; set; }

        public List<ConditionReport> conditions { get; set; }
    }
}
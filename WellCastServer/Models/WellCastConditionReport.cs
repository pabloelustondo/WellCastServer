using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionReport
    {
        public int _id { get; set; }
        public String condition_id { get; set; }
        public int risk;
    }
}
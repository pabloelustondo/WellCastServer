﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class ConditionReport
    {
        [Newtonsoft.Json.JsonIgnore]
        public int ID { get; set; }
        public String ConditionKey { get; set; }
        public int Risk;
    }
}
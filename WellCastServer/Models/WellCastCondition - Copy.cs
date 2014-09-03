using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Configuration
    {
        public int ConfigurationID {get;set;}
        public int MaxAgeMinutes { get; set; } //maximun age for a forecast in minutes
    }
}
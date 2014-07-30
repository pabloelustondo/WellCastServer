using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastEntity
    {
             [Newtonsoft.Json.JsonIgnore]
        public string ID { get; set; }

        public string _id { get { return ID; } }

        public string name { get; set; }

        public string description { get; set; }

    
      }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastRelation
    {
        [Newtonsoft.Json.JsonIgnore]
        public Guid ID { get; set; }
        public string _id { get { return ID.ToString(); } }

        public WellCastRelation() {

            ID = Guid.NewGuid();
        
        }
       
      }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastRelation
    {
        public Guid ID { get; set; }

        public WellCastRelation() {

            ID = Guid.NewGuid();
        
        }
       
      }
}
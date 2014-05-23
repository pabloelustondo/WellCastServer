using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public WellCastEntity()
        {

            ID = Guid.NewGuid();
        
        }
       
      }
}
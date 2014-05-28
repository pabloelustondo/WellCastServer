using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class Location: WellCastEntity
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public int x { get; set; }  //x on narr grid
        public int y { get; set; }  //y on narr grid     
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastLog
    {

        public int ID { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public DateTime timeStamp { get; set; }

    }
}
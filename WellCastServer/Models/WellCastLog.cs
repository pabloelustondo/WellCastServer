using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public DateTime timeStamp { get; set; }

    }
}
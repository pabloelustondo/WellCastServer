﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastEntityWithKeyName: WellCastEntity
    {
        [Newtonsoft.Json.JsonIgnore]
        public string KeyName { get; set; }       
      }
}
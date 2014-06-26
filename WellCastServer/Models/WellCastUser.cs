using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class User: WellCastEntity
    {
        public virtual List<String> ProfileIDs { get; set; }
        public virtual List<String> LocationIDs { get; set; }

        public virtual List<Location> Locations { get; set; }
        public virtual List<Profile> Profiles { get; set; }

    }
}
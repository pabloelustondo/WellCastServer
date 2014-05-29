using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class User: WellCastEntity
    {
        public int x { get; set; }
        public virtual List<Guid> LocationIDs
        {
            get
            {
                var _LocationIDs = new List<Guid>();
                if (Locations == null) return _LocationIDs;
                foreach (var cs in Locations)
                {
                    _LocationIDs.Add(cs.ID);
                }
                return _LocationIDs;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Location> Locations { get; set; }

        public virtual List<Guid> ProfileIDs
        {
            get
            {
                var _ProfileIDs = new List<Guid>();
                if (Profiles == null) return _ProfileIDs;
                foreach (var cs in Profiles)
                {
                    _ProfileIDs.Add(cs.ID);
                }
                return _ProfileIDs;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Profile> Profiles { get; set; }

    }
}
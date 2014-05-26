using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class WellCastEnvelope<T>
    {
        public WellCastMetadata meta { get; set; }  //error code
        public dynamic data { get; set; }

        public WellCastEnvelope(T t)
        {
            meta = new WellCastMetadata();
            data = t;
        }
    }


    public class WellCastMetadata
    {
        public string status { get; set; }
        public string access { get; set; }
        public string message { get; set; }

        public WellCastMetadata()
        { // returns de default everything is cool and read only
            status = "ok";
            access = "read-only";
        }
    }

    public class WellCastStatus {     
        public string code { get; set; }
        public string message { get; set; }
        public WellCastStatus(string _code, string _message){
            code = _code;
            message = _message;
        }
    }

    public class WellCastMessages {
        public static readonly WellCastStatus Ok = new WellCastStatus("Ok", "Everything is Ok");
        public static readonly WellCastStatus NonExistingId = new WellCastStatus("NonExistentId", "Cannot find entity associated with provided ID");
        public static readonly WellCastStatus InvalidId = new WellCastStatus("InvalidId", "The provided ID is not in the correct format");
        public static readonly WellCastStatus Exception = new WellCastStatus("Exception", "Message will be the actual exception");
 

    }
}
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellCastServer.Models
{
    public class MongoEntity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
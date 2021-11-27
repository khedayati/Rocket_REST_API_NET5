using System;
using System.Collections.Generic;

namespace RocketApi.Models
{
    public class BuildingDetails
    {
        // public virtual buildings building { get; set; }

        public long id { get; set; }
        public long building_id { get; set; }
        public string information_key { get; set; }
        public string value { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
using System;

namespace RocketApi.Models
{
    public class Addresses
    {
        public long id { get; set; }
        public string type_of_address { get; set; }
        public string status { get; set; }
        public string entity { get; set; }
        public string number_and_street { get; set; }
        public string suite_or_apartment { get; set;}
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country { get; set;}
        public string notes { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public long longitude { get; set; }
        public long latitude { get; set; }

    }
}
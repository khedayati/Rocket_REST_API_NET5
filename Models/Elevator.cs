using System;

namespace RocketApi.Models
{
    public class Elevators
    {
        public long id { get; set; }
        public long column_id { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public string elevator_type { get; set; }
        public string status { get; set; }
        public DateTime? date_of_commissioning { get; set; }
        public DateTime? date_of_last_inspection { get; set; }
        public string certificate_of_inspection { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
using System;

namespace RocketApi.Models
{
    public class fact_interventions
    {
        public long intervention_id { get; set; }
        public long employee_id { get; set; }
        public long building_id { get; set; }
        public long? battery_id { get; set; } 
        public long? column_id { get; set; }
        public long? elevator_id { get; set; }
        public DateTime intervention_start { get; set; }
        public DateTime? intervention_end { get; set; }
        public string result { get; set; }
        public string? report { get; set;}
        public string status { get; set; }
        
    }
}
using System;
using System.Collections.Generic;

namespace RocketApi.Models
{
  public class Battery
  {
    public long id { get; set; }
    public long building_id { get; set; }
    public string battery_type { get; set; }
    public string status { get; set; }
    public long employee_id { get; set; }
    public string Date_of_ { get; set; }
    public DateTime? date_of_the_last_inspection { get; set; }
    public string certificate_of_operations { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    public DateTime? updated_at { get; set; }
    public DateTime? date_of_commissioning { get; set; }



    public Boolean getColumnList(List<Column> filteredColumns, List<Elevator> filteredElevators)
    {
      var currentColumns = new List<Column>();
      foreach (Column column in filteredColumns)
      {
        if (column.battery_id == this.id && column.getElevatorList(filteredElevators))
        {
          currentColumns.Add(column);
        }

      }

      if (currentColumns.Count > 0)
      {
        return true;
      }
      return false;
    }
  }
}
using System;
using System.Collections.Generic;

namespace RocketApi.Models
{
  public class Column
  {
    public long id { get; set; }
    public string column_type { get; set; }
    public string status { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public long battery_id { get; set; }

    public Boolean getElevatorList(List<Elevator> filteredElevators)
    {
      var currentElevators = new List<Elevator>();
      foreach (Elevator elevator in filteredElevators)
      {
        if (elevator.column_id == this.id)
        {
          currentElevators.Add(elevator);
        }
      }

      if (currentElevators.Count > 0)
      {
        return true;
      }
      return false;
    }
  }
}
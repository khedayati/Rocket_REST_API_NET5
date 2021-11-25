using System;

namespace RocketApi.Models
{
  public class Building
  {

    public long id { get; set; }
    public long customer_id { get; set; }
    public long created_at { get; set; }
    public long updated_at { get; set; }
    public long address_id { get; set; }

  }
}
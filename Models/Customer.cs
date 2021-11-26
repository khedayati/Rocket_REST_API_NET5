using System;

namespace RocketApi.Models
{
  public class Customer
  {
    public long id { get; set; }
    public long user_id { get; set; }
    public DateTime? customer_creation_date { get; set; }
    public string company_name { get; set; }
    public string company_contact_phone { get; set; }
    public string company_description { get; set; }
    public string email_of_the_company_contact { get; set; }

  }
}
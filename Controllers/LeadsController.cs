using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RocketApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RocketApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeadsController : ControllerBase
  {
    private readonly ApplicationContext _context;

    public LeadsController(ApplicationContext context)
    {
      _context = context;
    }

    [HttpGet]
    public List<Lead> GetLeads()
    {
      //var leads = _context.leads;
      //var customers = _context.customers.ToList();
      List<Lead> notCustomers = new List<Lead>();

      DateTime currentDate = DateTime.Now;
      /*
      List<Lead> filteredLeads = _context.leads
                                .Where(lead => lead.date_of_creation > currentDate
                                .AddDays(Convert.ToDouble(-30)))
                                .ToList();
      List<Customer> filteredCustomers = _context.customers
                                         .Where(customer => customer.customer_creation_date > currentDate
                                         .AddDays(Convert.ToDouble(-30)))
                                         .ToList();

      foreach (Lead lead in _context.leads)
      {
        foreach (Customer customer in _context.customers)
        {
          if (lead.email != customer.Email && lead.phone != customer.company_contact_phone)
          {
            notCustomers.Add(lead);
            //return notCustomers;
          }
        }
      }
      */
      return notCustomers;
    }
  }
}
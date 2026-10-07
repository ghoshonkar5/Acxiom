using AcxiomCRM.Data.Contexts;
using AcxiomCRM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AcxiomCRM.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            var leads = await _context.Leads.ToListAsync();
            var opps = await _context.Opportunities.ToListAsync();

            ViewBag.TotalCustomers = customers.Count;
            ViewBag.ActiveCustomers = customers.Count(c => c.Status == "Active");
            ViewBag.InactiveCustomers = customers.Count(c => c.Status == "Inactive");

            ViewBag.TotalLeads = leads.Count;
            ViewBag.OpenLeads = leads.Count(l => l.Status == "New" || l.Status == "Contacted" || l.Status == "Qualified");
            ViewBag.HighPriorityLeads = leads.Count(l => l.Value > 100000);

            ViewBag.PipelineValue = opps.Where(o => o.Stage != "Closed Lost").Sum(o => o.Amount);
            ViewBag.WeightedValue = opps.Where(o => o.Stage == "Closed Won").Sum(o => o.Amount);
            ViewBag.OpenOpps = opps.Count(o => o.Stage != "Closed Won" && o.Stage != "Closed Lost");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

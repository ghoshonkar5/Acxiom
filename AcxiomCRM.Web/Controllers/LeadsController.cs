using AcxiomCRM.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Web.Controllers
{
    [Authorize]
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeadsController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() { return View(await _context.Leads.Include(l => l.Owner).ToListAsync()); }
    }
}

using AcxiomCRM.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Web.Controllers
{
    [Authorize]
    public class OpportunitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OpportunitiesController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() { return View(await _context.Opportunities.Include(o => o.Owner).Include(o => o.Customer).ToListAsync()); }
    }
}

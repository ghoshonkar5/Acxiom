using AcxiomCRM.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Web.Controllers
{
    [Authorize]
    public class FollowUpsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FollowUpsController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() { return View(await _context.FollowUps.Include(f => f.Lead).Include(f => f.Owner).ToListAsync()); }
    }

    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ActivitiesController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() { return View(await _context.Activities.Include(a => a.Owner).ToListAsync()); }
    }
}

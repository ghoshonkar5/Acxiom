using AcxiomCRM.Data.Contexts;
using AcxiomCRM.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AcxiomCRM.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager) { _userManager = userManager; }
        public async Task<IActionResult> Index() { return View(await _userManager.Users.ToListAsync()); }
    }

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager) { _roleManager = roleManager; }
        public async Task<IActionResult> Index() { return View(await _roleManager.Roles.ToListAsync()); }
    }

    [Authorize(Roles = "Admin,Manager")]
    public class AuditController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuditController(ApplicationDbContext context) { _context = context; }
        public async Task<IActionResult> Index() { return View(await _context.AuditLogs.OrderByDescending(a => a.Timestamp).Take(100).ToListAsync()); }
    }

    [Authorize]
    public class ReportsController : Controller
    {
        public IActionResult Index() { return View(); }
    }

    [Authorize]
    public class DeveloperController : Controller
    {
        public IActionResult Index() { return View(); }
    }
}

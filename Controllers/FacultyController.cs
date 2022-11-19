using NguyenVietPhuongBTH2.Data;
using NguyenVietPhuongBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NguyenVietPhuongBTH2.Models.Process;


namespace NguyenVietPhuongBTH2.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Faculty.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Faculty fct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fct);
        }
       
    }
}

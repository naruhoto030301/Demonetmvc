using NguyenVietPhuongBTH2.Data;
using NguyenVietPhuongBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NguyenVietPhuongBTH2.Models.Process;


namespace NguyenVietPhuongBTH2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employee.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private bool EmployeeExixsts(string id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var Employee = await _context.Employee.FindAsync(id);
            if (Employee == null)
            {
                return View("NotFound");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("EmployeeID,EmployeeName")] Employee emp)
        {
            if (id != emp.EmployeeID)
            {
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExixsts(emp.EmployeeID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }
        //GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var emp = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (emp == null)
            {
                return View("NotFound");
            }
            return View();
        }
        //POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var emp = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to sever
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                        //read data from file write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Employee object
                            var emp = new Employee();
                            //set values for attributes
                            emp.EmployeeID = dt.Rows[i][0].ToString();
                            emp.EmployeeName = dt.Rows[i][1].ToString();
                            emp.Address = dt.Rows[i][2].ToString();
                            //add object to Context
                            _context.Employee.Add(emp);

                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));


                    }
                }
            }
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZayShop.Models;
using ZayShop.Models.Data;

namespace ZayShop.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        //private readonly string StringConnection = "Server=DESKTOP-H0IHN30\\SQLEXPRESS; Database=ZayShop; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true;";
        //public IActionResult Index()
        //{
        //    List<StudentModel> students = new List<StudentModel>();
        //    using (SqlConnection conn = new SqlConnection(StringConnection))
        //    {
        //        string query = "select *from students";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            students.Add(new StudentModel
        //            {
        //                Id = (int)reader["Id"],
        //                FirstName = reader["Firstname"].ToString(),
        //                LastName = reader["Lastname"].ToString(),
        //                Email = reader["Email"].ToString(),
        //                Age = (int)reader["Age"]
        //            });
        //        }
        //    }

        //    return View(students);
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _context.Studentss.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                _context.Studentss.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //Get student/edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Studentss.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentModel student, int id)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Studentss.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Studentss.FirstOrDefaultAsync(m=>m.Id==id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Studentss.FindAsync(id);
            if (student != null)
            {
                _context.Studentss.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}

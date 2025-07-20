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


    }
}

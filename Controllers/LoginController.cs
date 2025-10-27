
using CRM_ManagementInterface.Data;
using Microsoft.AspNetCore.Mvc;
using CRM_ManagementInterface.Models;


namespace CRM_ManagementInterface.Controllers
{
    public class LoginController : Controller
    {

        private readonly CRMContext _context;

        public LoginController(CRMContext context)
        {
            _context = context;
        }

        // GET: Login/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login/Index
        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View("Index");
            }

            // Check the database for matching credentials
            var user = _context.LoginDetails.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

            if (user != null)
            {
                // Successful login - redirect to a dashboard or home page
                return RedirectToAction("Index", "Client"); // Redirect to Clients page
            }
            else
            {
                // Invalid credentials
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Index");
            }
        }

    }

}

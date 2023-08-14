using EmployeeCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BOL;
using DAL;

namespace EmployeeCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult GetEmployeeList()
        {
            List<Employee> employees = DBManager.GetAllEmployees();

            ViewData["emps"] = employees;
            return View();
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(string id, string firstName, string lastName, string email, string salary, string dob, string password, string dept) {
            //Console.WriteLine(id+firstName+lastName+email+salary+dob+password+dept);
            Employee emp = new Employee(int.Parse(id), firstName, lastName, email, Double.Parse(salary), DateTime.Parse(dob), password, Enum.Parse<Department>(dept));
            //Console.WriteLine(emp);
            DBManager.AddEmployee(emp);

            return RedirectToAction("GetEmployeeList");
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int delId) {

            DBManager.DeleteEmployee(delId);

            return RedirectToAction("GetEmployeeList");

        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int id)
        {
            ViewData["emp"] = DBManager.GetEmployee(id);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateEmployeeById(string id, string firstName, string lastName, string email, string salary, string dob, string password, string dept)
        {
            Employee emp = new Employee(int.Parse(id), firstName, lastName, email, Double.Parse(salary), DateTime.Parse(dob), password, Enum.Parse<Department>(dept));
            DBManager.UpdateEmployeeById(emp);
            
            return RedirectToAction("GetEmployeeList");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
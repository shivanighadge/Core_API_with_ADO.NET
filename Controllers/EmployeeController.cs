using API_CRUD_ADO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext context;


        public EmployeeController(EmployeeDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
             var employees = context.GetEmployees().ToList();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            var result = context.AddEmployees(emp);
            return Ok(result);
        }
    }
}

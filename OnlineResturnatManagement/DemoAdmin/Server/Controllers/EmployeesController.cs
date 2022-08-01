
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private ILoggerManager _logger;
        private ICashHelper<Employee> _cashHelper;
        public EmployeesController(IEmployeeService employeeService, ILoggerManager logger, ICashHelper<Employee> cashHelper)
        {
            _employeeService = employeeService;
            _logger = logger;
            _cashHelper = cashHelper;   
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var cacheKey = "customerList";
                var employeeList = new List<Employee>();
                //_cashHelper.RemoveDataAsync(cacheKey);
               employeeList = _cashHelper.GetDataAsync(cacheKey).Result;
                
                if (employeeList.Count<=0)
                {
                    employeeList = (List<Employee>)await _employeeService.GetAllEmployeeAsync();
                    
                    _cashHelper.SetDataAsync(cacheKey,employeeList);
                   
                }
               
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                
                var empdata = _cashHelper.GetSingleDataAsync("empObj_" + id.ToString()).Result;

                Employee emp = new Employee();
                if (empdata == null)
                {
                    emp = await _employeeService.GetEmployeeByIdAsync(id);
                    _cashHelper.SetDataAsync("empObj_" + id.ToString(),emp);
                }
                else
                {
                    emp = empdata;  

                }
                if (emp == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    return Ok(emp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _employeeService.CreateEmployee(employee);

                return CreatedAtRoute("EmployeeById", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var employeeEntity = await _employeeService.GetEmployeeByIdAsync(id);
                if (employeeEntity == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                
                _employeeService.UpdateEmployee(employee);
               
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _employeeService.DeleteEmployee(employee);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

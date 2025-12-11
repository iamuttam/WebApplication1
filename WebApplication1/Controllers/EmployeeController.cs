using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeDBContax _employeeDBContax;
        public EmployeeController(ILogger<EmployeeController> logger, EmployeeDBContax employeeDBContax)
        {
            _logger = logger;
           _employeeDBContax = employeeDBContax;


        }
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public ActionResult< IEnumerable<EmployeeDTO>> getAllEmployee()
        {
            _logger.LogInformation("Get All Employee Method Started..");
           var employees = new List <EmployeeDTO>();
            foreach (var item in _employeeDBContax.employees)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO()
                {
                    EmployeeId = item.EmployeeId,
                    EmployeeName = item.EmployeeName,
                    Email = item.Email,
                    EmployeeAge = item.EmployeeAge,
                    Gender = item.Gender,
                    Experience = item.Experience,
                    Department = item.Department,
                    Description = item.Description
                };
                employees.Add(employeeDTO);
            }

            var employees1 = _employeeDBContax.employees.Select(e => new EmployeeDTO()
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                EmployeeAge = e.EmployeeAge,
                Email = e.Email,
                Gender = e.Gender,
                Experience = e.Experience,
                Department = e.Department,
                Description = e.Description
            });
            return Ok(employees);
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> Createmployee([FromBody]EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Bad Request..");
                return BadRequest(ModelState);
            }

            //if(employeeDTO.EmployeeAge < 18 || employeeDTO.EmployeeAge >60)
            //{
            //    ModelState.AddModelError("AgeValidation Error","Employee Age Should Be more Than 18 and less tha 61.");
            //    return BadRequest(ModelState);
            //}

            if (employeeDTO == null)
            {
                _logger.LogWarning("Bad Request..");
                return BadRequest();
            }
            //int newEMpId = _employeeDBContax.employees.LastOrDefault().EmployeeId + 1;
            Employee employee = new Employee
            {
                //EmployeeId = newEMpId,
                EmployeeName = employeeDTO.EmployeeName,
                Email = employeeDTO.Email,
                EmployeeAge = employeeDTO.EmployeeAge,
                Gender = employeeDTO.Gender,
                Department = employeeDTO.Department,
                Experience = employeeDTO.Experience,
                Description = employeeDTO.Description
            };
            _employeeDBContax.employees.Add(employee);
            _employeeDBContax.SaveChanges();
            employeeDTO.EmployeeId = employee.EmployeeId;
            return CreatedAtRoute("GetStudentbyId",new{ id = employeeDTO.EmployeeId},employeeDTO);
 

        }

        //[HttpGet("{id:int}", Name = "GetStudentbyId")]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentbyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> getEmployeeById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request..");
                return BadRequest();
            }
            var employees = _employeeDBContax.employees.Where(a => a.EmployeeId == id).FirstOrDefault();
            if (employees == null)
            {
                _logger.LogError("Student Not Found with Given Id.");
                return NotFound($"The Employee with ID {id} Not found");
            }
        
            var employeeDTO = new EmployeeDTO
            {
                EmployeeId= employees.EmployeeId,
                EmployeeName = employees.EmployeeName,
                Email = employees.Email,
                EmployeeAge = employees.EmployeeAge,
                Gender = employees.Gender,
                Experience = employees.Experience,
                Department = employees.Department,
                Description = employees.Description,
            };
            
                return Ok(employeeDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentbyName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult <EmployeeDTO> getEmployeeByName(string name)
        {
            if (name == "")
                return BadRequest();
            var employees = _employeeDBContax.employees.Where(a => a.EmployeeName == name).FirstOrDefault();
            if (employees == null )
                return NotFound($"The Employee with Name {name} Not found");
            var employeeDTO = new EmployeeDTO
            {
                EmployeeId = employees.EmployeeId,
                EmployeeName = employees.EmployeeName,
                Email = employees.Email,
                EmployeeAge = employees.EmployeeAge,
                Gender = employees.Gender,
                Experience = employees.Experience,
                Department = employees.Department,
            };
            return Ok(employeeDTO);
        }

        [HttpDelete("{EmpId}", Name ="DeleteEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  ActionResult <bool> DeleteEmployee(int EmpId)
        {
            if (EmpId <= 0)
                return BadRequest();
            var emp = _employeeDBContax.employees.Where(a => a.EmployeeId == EmpId).FirstOrDefault();

            if (emp == null)
            {
                return NotFound($"The Employee with ID {EmpId} Not found");
            }
            else
            {
                _employeeDBContax.employees.Remove(emp);
                _employeeDBContax.SaveChanges();
                return Ok();
            }
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> UpdateEmployee([FromBody] EmployeeDTO model)
        {
            if(model == null && model.EmployeeId  <= 0)
            {
                return NotFound();
            }

            var emp = _employeeDBContax.employees.Where(a => a.EmployeeId == model.EmployeeId).FirstOrDefault();
            if(emp == null)
                return NotFound();
            emp.EmployeeName = model.EmployeeName;
            emp.Email = model.Email;
            emp.EmployeeAge = model.EmployeeAge;
            emp.Experience = model.Experience;
            emp.Department = model.Department;
            emp.Description = model.Description;
            _employeeDBContax.SaveChanges();
          return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> UpdateEmployeePartial(int id,[FromBody] JsonPatchDocument<EmployeeDTO> patchDocument)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var emp = _employeeDBContax.employees.Where(a => a.EmployeeId == id).FirstOrDefault();

            var employeeDTO = new EmployeeDTO
            {
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName,
                Email = emp.Email,
                EmployeeAge = emp.EmployeeAge,
                Experience = emp.Experience,
                Department = emp.Department,
                Description = emp.Description,
                
            };
            patchDocument.ApplyTo(employeeDTO,ModelState);

            if (emp == null)
                return NotFound();
            emp.EmployeeName = employeeDTO.EmployeeName;
            emp.Email = employeeDTO.Email;
            emp.EmployeeAge = employeeDTO.EmployeeAge;
            emp.Experience = employeeDTO.Experience;
            emp.Department = employeeDTO.Department;
            emp.Description = employeeDTO.Description;
            _employeeDBContax.SaveChanges();
            return NoContent();
        }
    }
}

using AutoMapper;
using Azure;
using Humanizer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(PolicyName = "AllowAll")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        //private readonly EmployeeDBContax _employeeDBContax;
        private readonly IMapper _mapper;
        //private readonly IEmployeeRepository _employeeRepository;
        private  readonly ICompanyRepository<Employee> _employeeRepository;
        public EmployeeController(ILogger<EmployeeController> logger, /*EmployeeDBContax employeeDBContax,*/ IMapper mapper, ICompanyRepository<Employee> employeeRepository)
        {
            _logger = logger;
          // _employeeDBContax = employeeDBContax;
            _mapper = mapper;
            _employeeRepository = employeeRepository;

        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public async Task<ActionResult< IEnumerable<EmployeeDTO>>> getAllEmployee()
        {
            _logger.LogInformation("Get All Employee Method Started..");
            //var employees = new List<EmployeeDTO>();
            //foreach (var item in _employeeDBContax.employees)
            //{
            //    EmployeeDTO employeeDTO = new EmployeeDTO()
            //    {
            //        EmployeeId = item.EmployeeId,
            //        EmployeeName = item.EmployeeName,
            //        Email = item.Email,
            //        EmployeeAge = item.EmployeeAge,
            //        Gender = item.Gender,
            //        Experience = item.Experience,
            //        Department = item.Department,
            //        Description = item.Description
            //    };
            //    employees.Add(employeeDTO);
            //}

            //var employees =await _employeeDBContax.employees.ToListAsync();
            //var employees = await _employeeDBContax.employees.Select(e => new EmployeeDTO()
            //{
            //    EmployeeId = e.EmployeeId,
            //    EmployeeName = e.EmployeeName,
            //    EmployeeAge = e.EmployeeAge,
            //    Email = e.Email,
            //    Gender = e.Gender,
            //    Experience = e.Experience,
            //    Department = e.Department,
            //    Description = e.Description
            //}).ToListAsync();

            var employees = await _employeeRepository.GetALLEmplooyeeAsync();

            var employeeDTOData =  _mapper.Map<List<EmployeeDTO>>(employees);
            return Ok(employeeDTOData);

        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> CreatemployeeAsync([FromBody]EmployeeDTO employeeDTO)
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
            //int newEMpId = await _employeeRepository.GetEmployeeByIdAsync() + 1;
            var employee_DTO = _mapper.Map<Employee>(employeeDTO);

          var EmployeeId=  await _employeeRepository.CreateNewEmployeeAsync(employee_DTO);

           // EmployeeDTO employeeDTOData = await _employeeRepository.CreateNewEmployeeAsync(employee);
            return CreatedAtRoute("GetStudentbyId",new{ id = employee_DTO.EmployeeId }, employee_DTO);
            

        }

        //[HttpGet("{id:int}", Name = "GetStudentbyId")]
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentbyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> getEmployeeByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request..");
                return BadRequest();
            }
           // var employees = await _employeeDBContax.employees.Where(a => a.EmployeeId == id).FirstOrDefaultAsync();
           
            var employees = await _employeeRepository.GetEmployeeByIdAsync(employee => employee.EmployeeId == id);
            if (employees == null)
            {
                _logger.LogError("Student Not Found with Given Id.");
                return NotFound($"The Employee with ID {id} Not found");
            }
        
            //var employeeDTO = new EmployeeDTO
            //{
            //    EmployeeId= employees.EmployeeId,
            //    EmployeeName = employees.EmployeeName,
            //    Email = employees.Email,
            //    EmployeeAge = employees.EmployeeAge,
            //    Gender = employees.Gender,
            //    Experience = employees.Experience,
            //    Department = employees.Department,
            //    Description = employees.Description,
            //};
            var employeeDTO= _mapper.Map<EmployeeDTO>(employees);
            
                return Ok(employeeDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentbyName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult <EmployeeDTO>> getEmployeeByName(string name)
        {
            if (name == "")
                return BadRequest();
            //var employees = await  _employeeDBContax.employees.Where(a => a.EmployeeName == name).FirstOrDefaultAsync();
            var employees =await _employeeRepository.GetEmployeeByNameAsync(student => student.EmployeeName.ToLower().Contains(name));
            if (employees == null )
            return NotFound($"The Employee with Name {name} Not found");
            //var employeeDTO = new EmployeeDTO
            //{
            //    EmployeeId = employees.EmployeeId,
            //    EmployeeName = employees.EmployeeName,
            //    Email = employees.Email,
            //    EmployeeAge = employees.EmployeeAge,
            //    Gender = employees.Gender,
            //    Experience = employees.Experience,
            //    Department = employees.Department,
            //};

            var employeeDTO = _mapper.Map<EmployeeDTO>(employees);
            return Ok(employeeDTO);
        }

        [HttpDelete("{EmpId}", Name ="DeleteEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  async Task<ActionResult<bool>> DeleteEmployeeAsync(int EmpId)
        {
            if (EmpId <= 0)
                return BadRequest();
            //var emp = await _employeeDBContax.employees.Where(a => a.EmployeeId == EmpId).FirstOrDefaultAsync();
            var emp = await _employeeRepository.GetEmployeeByIdAsync(employee=>employee.EmployeeId ==EmpId);

            if (emp == null)
            {
                return NotFound($"The Employee with ID {EmpId} Not found");
            }
            else
            {
                await _employeeRepository.DeleteEmployeeAsync(emp);
                return Ok(true);

            }

           
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeeAsync([FromBody] EmployeeDTO dto)
        {
            if(dto == null && dto.EmployeeId  <= 0)
            {
                return NotFound();
            }

            // var existingRecord = await _employeeDBContax.employees.AsNoTracking().Where(a => a.EmployeeId == dto.EmployeeId).FirstOrDefaultAsync();
            
            
            var existingRecord = await _employeeRepository.GetEmployeeByIdAsync(employee =>employee.EmployeeId == dto.EmployeeId);
            if (existingRecord == null)
                return NotFound();
            //emp.EmployeeName = model.EmployeeName;
            //emp.Email = model.Email;
            //emp.EmployeeAge = model.EmployeeAge;
            //emp.Experience = model.Experience;
            //emp.Department = model.Department;
            //emp.Description = model.Description;

            var newRecord= _mapper.Map<Employee>(dto);
            //_employeeDBContax.Update(newRecord);           
            //await _employeeDBContax.SaveChangesAsync();

            var updateRecord = await _employeeRepository.UpdateEmployeeAsync(newRecord);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeePartialAsync(int id,[FromBody] JsonPatchDocument<EmployeeDTO> patchDocument)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            //var existingEmployee = await _employeeDBContax.employees.AsNoTracking().Where(a => a.EmployeeId == id).FirstOrDefaultAsync();
            var existingEmployee =await _employeeRepository.GetEmployeeByIdAsync(employee => employee.EmployeeId == id);

            //var employeeDTO = new EmployeeDTO
            //{
            //    EmployeeId = emp.EmployeeId,
            //    EmployeeName = emp.EmployeeName,
            //    Email = emp.Email,
            //    EmployeeAge = emp.EmployeeAge,
            //    Experience = emp.Experience,
            //    Department = emp.Department,
            //    Description = emp.Description,

            //};
            var employeeDTO = _mapper.Map<EmployeeDTO>(existingEmployee); 
            patchDocument.ApplyTo(employeeDTO,ModelState);

            if (existingEmployee == null)
                return NotFound();
            //existingEmployee.EmployeeName = employeeDTO.EmployeeName;
            //existingEmployee.Email = employeeDTO.Email;
            //existingEmployee.EmployeeAge = employeeDTO.EmployeeAge;
            //existingEmployee.Experience = employeeDTO.Experience;
            //existingEmployee.Department = employeeDTO.Department;
            //existingEmployee.Description = employeeDTO.Description;

            existingEmployee=_mapper.Map<Employee>(employeeDTO);
            //_employeeDBContax.Update(existingEmployee);
            //_employeeDBContax.SaveChangesAsync();
            await _employeeRepository.UpdateEmployeeAsync(existingEmployee );
            return NoContent();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OEMS.Application.Commands.Employee.AddEmployee;
using OEMS.Application.Commands.Employee.DeleteEmployee;
using OEMS.Application.Commands.Employee.UpdateEmployee;
using OEMS.Application.Models;
using OEMS.Application.Models.Employee;
using OEMS.Application.Queries.Employee.GetEmployeeItem;
using OEMS.Application.Queries.Employee.GetEmployeeList;
using OEMS.Web.Extensions;
using System;
using System.Threading.Tasks;

namespace OEMS.Web.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CustomPagedList<EmployeeModel>>> GetEmployeeListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}",
                nameof(GetEmployeeListAsync), pageIndex, pageSize);
            try
            {
                var request = new GetEmployeeListRequest
                {
                    SearchKey = searchKey,
                    OrderBy = orderBy,
                    SortBy = sortBy,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(GetEmployeeListAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeItemAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(GetEmployeeItemAsync), id);
            try
            {
                var request = new GetEmployeeItemRequest
                {
                    Id = id
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(GetEmployeeItemAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployeeAsync(string userName, EmployeeModel employee)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: Employee={Employee}", nameof(UpdateEmployeeAsync), employee);
            try
            {
                var request = new UpdateEmployeeRequest
                {
                    Employee = employee,
                    Username = userName
                };
                await _mediator.Send(request);

                var updatedEmployeeRequest = new GetEmployeeItemRequest
                {
                    Id = employee.Id
                };
                return await _mediator.Send(updatedEmployeeRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(UpdateEmployeeAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> AddEmployeeAsync(string userName, EmployeeModel employee)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: Employee={Employee}", nameof(AddEmployeeAsync), employee);
            try
            {
                var request = new AddEmployeeRequest
                {
                    Employee = employee,
                    Username = userName
                };
                return await _mediator.Send(request);          
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(AddEmployeeAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(DeleteEmployeeAsync), id);
            try
            {
                var request = new DeleteEmployeeRequest
                {
                    Id = id
                };
                await _mediator.Send(request);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(DeleteEmployeeAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }
    }
}

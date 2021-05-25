using MediatR;
using OEMS.Application.Models.Employee;

namespace OEMS.Application.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeRequest : IRequest
    {
        public EmployeeModel Employee { get; set; }
        public string Username { get; set; }        
    }
}

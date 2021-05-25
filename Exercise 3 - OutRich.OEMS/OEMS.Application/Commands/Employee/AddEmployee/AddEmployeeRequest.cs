using MediatR;
using OEMS.Application.Models.Employee;

namespace OEMS.Application.Commands.Employee.AddEmployee
{
    public class AddEmployeeRequest : IRequest<EmployeeModel>
    {
        public EmployeeModel Employee { get; set; }
        public string Username { get; set; }        
    }
}

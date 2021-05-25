using MediatR;

namespace OEMS.Application.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeRequest : IRequest
    {
        public int Id { get; set; }
    }
}

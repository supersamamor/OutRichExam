using MediatR;
using OEMS.Application.Models.Employee;

namespace OEMS.Application.Queries.Employee.GetEmployeeItem
{
    public class GetEmployeeItemRequest : IRequest<EmployeeModel>
    {
        public int Id { get; set; }
    }
}

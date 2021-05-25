using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OEMS.Data.Repositories;
using OEMS.Application.Models.Employee;

namespace OEMS.Application.Queries.Employee.GetEmployeeItem
{
    public class GetEmployeeItemRequestHandler : IRequestHandler<GetEmployeeItemRequest, EmployeeModel>
    {
        private readonly EmployeeRepository _repository;
        private readonly IMapper _mapper;
        public GetEmployeeItemRequestHandler(EmployeeRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<EmployeeModel> Handle(GetEmployeeItemRequest request, CancellationToken cancellationToken)
        {
            var employeeCore = await _repository.GetItemAsync(request.Id);
            return _mapper.Map<Core.Models.Employee, EmployeeModel>(employeeCore); 
        }
    }
}

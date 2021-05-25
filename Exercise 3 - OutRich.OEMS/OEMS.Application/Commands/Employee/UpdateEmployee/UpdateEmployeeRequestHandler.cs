using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OEMS.Data;
using OEMS.Data.Repositories;

namespace OEMS.Application.Commands.Employee.UpdateEmployee
{  
    public class UpdateEmployeeRequestHandler : AsyncRequestHandler<UpdateEmployeeRequest>
    {
        private readonly EmployeeRepository _repository;
        private readonly OEMSContext _context;
        private readonly IMapper _mapper;
        public UpdateEmployeeRequestHandler(EmployeeRepository repository, OEMSContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        protected override async Task Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employeeCore = await _repository.GetItemAsync(request.Employee.Id);
            employeeCore.UpdateFrom(request.Employee.FirstName, request.Employee.LastName, request.Employee.BirthDate);
            employeeCore.SetUpdatedInformation(request.Username);
            await _repository.SaveAsync(employeeCore);
            await _context.SaveChangesAsync();          
        }

        public async Task HandleAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}

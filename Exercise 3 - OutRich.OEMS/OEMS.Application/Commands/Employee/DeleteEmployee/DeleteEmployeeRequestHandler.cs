using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OEMS.Data;
using OEMS.Data.Repositories;

namespace OEMS.Application.Commands.Employee.DeleteEmployee
{  
    public class DeleteEmployeeRequestHandler : AsyncRequestHandler<DeleteEmployeeRequest>
    {
        private readonly EmployeeRepository _repository;
        private readonly OEMSContext _context;     
        public DeleteEmployeeRequestHandler(EmployeeRepository repository, OEMSContext context) 
        {
            _repository = repository;
            _context = context;          
        }

        protected override async Task Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employeeCore = await _repository.GetItemAsync(request.Id);
            _repository.Delete(employeeCore);
            await _context.SaveChangesAsync();
        }

        public async Task HandleAsync(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace OEMS.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly OEMSContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(OEMSContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }    

        public async Task<Data.Models.Employee> SaveAsync(Core.Models.Employee employeeCore) 
        {
            var employee = _mapper.Map<Core.Models.Employee, Models.Employee>(employeeCore);
            if (employee.Id == 0)
            {
                await _context.Employee.AddAsync(employee);
            }
            else {
                _context.Entry(employee).State = EntityState.Modified;
            }   
            return employee;
        }

        public void Delete(Core.Models.Employee employeeCore)
        {
            var employee = _mapper.Map<Core.Models.Employee, Models.Employee>(employeeCore);
            _context.Employee.Remove(employee);
        }

        public async Task<Core.Models.Employee> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.Employee, Core.Models.Employee>(await _context.Employee.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OEMS.Data.Repositories
{
    public class UserRepository
    {
        private readonly OEMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(OEMSContext context, MapperConfiguration mapperConfig, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
            _userManager = userManager;
        }    

        public async Task<Data.Models.OEMSUser> SaveAsync(Core.Models.OEMSUser userCore) {
            var user = _mapper.Map<Core.Models.OEMSUser, Models.OEMSUser>(userCore);
            if (user.Id == 0)
            {
                await _context.OEMSUser.AddAsync(user);
            }
            else {
                _context.Entry(user).State = EntityState.Modified;
                _context.Entry(user.Identity).State = EntityState.Modified;
            }   
            return user;
        }

        public void Delete(Core.Models.OEMSUser userCore)
        {
            var user = _mapper.Map<Core.Models.OEMSUser, Models.OEMSUser>(userCore);
            _context.OEMSUser.Remove(user);
        }

        public async Task<Core.Models.OEMSUser> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.OEMSUser, Core.Models.OEMSUser>
                (await _context.OEMSUser.Include(l=>l.Identity).Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }

        public async Task<IList<string>> GetUserRoles(int id)
        {
            var identity = (await _context.OEMSUser.Include(l => l.Identity).Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync())?.Identity;
            return await _userManager.GetRolesAsync(identity);      
        }
    }     
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OEMS.Data.Models;
using System.Linq;

namespace OEMS.Data
{
    public class OEMSContext : IdentityDbContext<IdentityUser>
    {
        public OEMSContext(DbContextOptions<OEMSContext> options)
            : base(options)
        {         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Is Unique
			
            #endregion

            #region Disable Cascade Delete
            var cascadeFKs = builder.Model.GetEntityTypes()
           .SelectMany(t => t.GetForeignKeys())
           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            #endregion

          
            base.OnModelCreating(builder);
        }
        public virtual DbSet<OEMSUser> OEMSUser { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
          
        public virtual DbSet<OEMSApiClient> OEMSApiClient { get; set; }     
    }
}

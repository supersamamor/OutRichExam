namespace OEMS.Core.Models
{
    public class OEMSUser : BaseModel
    {   
        public string FullName { get; private set; }
        public IdentityUser Identity { get; private set; }
        public void UpdateFrom(string fullName, string email) {
            this.FullName = fullName;
            this.Identity.UpdateFrom(email);
        }
    }
}

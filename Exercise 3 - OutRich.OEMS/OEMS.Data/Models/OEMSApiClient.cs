using System;

namespace OEMS.Data.Models
{
    public class OEMSApiClient : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}

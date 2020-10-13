using System.Collections.Generic;
using LazyDev.EFCore.Entities;

namespace Rock.Core.Entities
{
    public class Account : EntityBase
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public List<AccountClaim> AccountClaims { get; set; }
    }
}

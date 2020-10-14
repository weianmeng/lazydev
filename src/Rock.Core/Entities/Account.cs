using System.Collections.Generic;

namespace Rock.Core.Entities
{
    public class Account : Entity
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public AccountStatus Status { get; set; }

        public List<AccountClaim> AccountClaims { get; set; }
    }

    public enum AccountStatus
    {
        Activated,
        Disabled,
        Locked
    }
}

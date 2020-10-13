using LazyDev.EFCore.Entities;

namespace Rock.Core.Entities
{
    public class AccountClaim:EntityBase
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Account Account { get; set; }
    }
}

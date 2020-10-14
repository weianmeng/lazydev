using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rock.Core.Entities;

namespace Rock.Infrastructure.Configurations
{
    public class AccountClaimConfiguration: IEntityTypeConfiguration<AccountClaim>
    {
        public void Configure(EntityTypeBuilder<AccountClaim> builder)
        {
            builder.ToTable("account_claim");

            builder.Property(x => x.Value).HasColumnName("value").HasMaxLength(20);
            builder.Property(x => x.Type).HasColumnName("type").HasMaxLength(10);
            builder.Property(x => x.AccountId).HasColumnName("account_id");

            builder.HasOne(b => b.Account)
                .WithMany(b => b.AccountClaims)
                .HasForeignKey(x=>x.AccountId);
        }
    }
}

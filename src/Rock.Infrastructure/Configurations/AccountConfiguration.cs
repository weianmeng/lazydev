using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rock.Core.Entities;

namespace Rock.Infrastructure.Configurations
{
    public  class AccountConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.Property(x => x.NickName).HasColumnName("nick_name").HasMaxLength(20);
            builder.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(15);
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(20);
            builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(15);
            builder.Property(x => x.Salt).HasColumnName("salt").HasMaxLength(6);
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.TenantId).HasColumnName("tenant_id");

            builder.HasMany(b=>b.AccountClaims).WithOne().HasForeignKey(x=>x.AccountId);
        }
    }
}

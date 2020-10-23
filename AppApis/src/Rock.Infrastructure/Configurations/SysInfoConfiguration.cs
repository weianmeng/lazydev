using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rock.Core.Entities;

namespace Rock.Infrastructure.Configurations
{
    public class SysInfoConfiguration: IEntityTypeConfiguration<SysInfo>
    {
        public void Configure(EntityTypeBuilder<SysInfo> builder)
        {
            builder.ToTable("sys_info");
            builder.Property(x => x.Version).HasColumnName("version").HasMaxLength(20);
        }
    }
}

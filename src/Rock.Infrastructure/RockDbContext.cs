﻿using LazyDev.Core.Runtime;
using LazyDev.EFCore;
using Microsoft.EntityFrameworkCore;
using Rock.Core.Entities;
using System.Reflection;

namespace Rock.Infrastructure
{
    public class RockDbContext: DbContextBase
    {
        public RockDbContext(DbContextOptions options, ILazyDevSession lazyDevSession) : base(options, lazyDevSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SysInfo> SysInfos { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }

    }
}
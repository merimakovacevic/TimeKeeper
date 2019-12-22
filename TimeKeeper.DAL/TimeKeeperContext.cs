using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        private string _conStr;
        //The default connection string in the constructor below would be for context calls outside of API
        public TimeKeeperContext() : base() { _conStr = "User ID=postgres; Password=postgres; Server=localhost; Port=5432; Database=TimeKeeper; Integrated Security=true; Pooling=true;"; }
        public TimeKeeperContext(DbContextOptions<TimeKeeperContext> options) : base(options) { }
        public TimeKeeperContext(string conStr)
        {
            _conStr = conStr;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<Day> Calendar { get; set; }
        public DbSet<DayType> DayTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<EmploymentStatus> EmploymentStatuses { get; set; }
        public DbSet<JobDetail> Tasks { get; set; }
        public DbSet<PricingStatus> PricingStatuses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MemberStatus> MemberStatuses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if(_conStr != null)
            {
                optionBuilder.UseNpgsql(_conStr);
            }
            optionBuilder.UseLazyLoadingProxies(true);
            base.OnConfiguring(optionBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Customer>().OwnsOne(x => x.HomeAddress);
            builder.Entity<Customer>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Member>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<CustomerStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Day>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<DayType>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Employee>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<EmployeePosition>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<EmploymentStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<JobDetail>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<PricingStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Project>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<ProjectStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Role>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Team>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<User>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<MemberStatus>().HasQueryFilter(x => !x.Deleted);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is BaseClass))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["Deleted"] = true;
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is BaseClass))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["Deleted"] = true;
            }
            return await base.SaveChangesAsync(token);
        }
    }
}

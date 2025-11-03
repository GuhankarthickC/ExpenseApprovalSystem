using ExpenseApprovalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ApprovalHistory> ApprovalHistories { get; set; }
        public DbSet<ApprovalRule> ApprovalRules { get; set; }
        public DbSet<ApprovalRuleLevel> ApprovalRuleLevels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.MinAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.MaxAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Head)
                .WithMany()
                .HasForeignKey(d => d.HeadId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)
                .WithMany()
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<ApprovalHistory>()
                .HasOne(a => a.Approver)
                .WithMany()
                .HasForeignKey(a => a.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Simple password for testing
            const string defaultPassword = "password123";

            // Seed initial data 
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Employee" },
                new Role { Id = 2, Name = "Manager" },
                new Role { Id = 3, Name = "DepartmentHead" },
                new Role { Id = 4, Name = "CFO" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Engineering", HeadId = null },
                new Department { Id = 2, Name = "Marketing", HeadId = null }
            );


            modelBuilder.Entity<User>().HasData(
                // Employees
                new User { Id = 1, Username = "john", PasswordHash = defaultPassword, Name = "John Doe", Email = "john@company.com", RoleId = 1, DepartmentId = 1, ManagerId = 4 },
                new User { Id = 2, Username = "jane", PasswordHash = defaultPassword, Name = "Jane Smith", Email = "jane@company.com", RoleId = 1, DepartmentId = 1, ManagerId = 4 },
                new User { Id = 3, Username = "bob", PasswordHash = defaultPassword, Name = "Bob Wilson", Email = "bob@company.com", RoleId = 1, DepartmentId = 2, ManagerId = 6 },

                // Managers
                new User { Id = 4, Username = "alice", PasswordHash = defaultPassword, Name = "Alice Brown", Email = "alice@company.com", RoleId = 2, DepartmentId = 1, ManagerId = 7 }, // Manager of Eng, reports to Sarah
                new User { Id = 6, Username = "mike", PasswordHash = defaultPassword, Name = "Mike Lee", Email = "mike@company.com", RoleId = 2, DepartmentId = 2, ManagerId = 5 }, // Manager of Marketing, reports to David

                // Department Heads
                new User { Id = 5, Username = "david", PasswordHash = defaultPassword, Name = "David Kim", Email = "david@company.com", RoleId = 3, DepartmentId = 2, ManagerId = null }, // Head of Marketing
                new User { Id = 7, Username = "sarah", PasswordHash = defaultPassword, Name = "Sarah Connor", Email = "sarah@company.com", RoleId = 3, DepartmentId = 1, ManagerId = null }, // Head of Engineering

                // CFO
                new User { Id = 8, Username = "cfo", PasswordHash = defaultPassword, Name = "CFO", Email = "cfo@company.com", RoleId = 4, DepartmentId = 1, ManagerId = null }
            );

            // ---------- Approval Rules (Configurable) ----------
            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule { Id = 1, MinAmount = 0, MaxAmount = 5000 },
                new ApprovalRule { Id = 2, MinAmount = 5001, MaxAmount = 20000 },
                new ApprovalRule { Id = 3, MinAmount = 20001, MaxAmount = null }
            );

            modelBuilder.Entity<ApprovalRuleLevel>().HasData(
                // Rule 1: ≤ 5000 → Manager
                new { Id = 1, RuleId = 1, Sequence = 1, ApproverType = "Manager" },

                // Rule 2: 5001–20000 → Manager → Dept Head
                new { Id = 2, RuleId = 2, Sequence = 1, ApproverType = "Manager" },
                new { Id = 3, RuleId = 2, Sequence = 2, ApproverType = "DepartmentHead" },

                // Rule 3: >20000 → Manager → Dept Head → CFO
                new { Id = 4, RuleId = 3, Sequence = 1, ApproverType = "Manager" },
                new { Id = 5, RuleId = 3, Sequence = 2, ApproverType = "DepartmentHead" },
                new { Id = 6, RuleId = 3, Sequence = 3, ApproverType = "CFO" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

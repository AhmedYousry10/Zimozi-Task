using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Models
{
    public class appContext : DbContext
    {
        public appContext() { }
        public appContext(DbContextOptions<appContext> options) : base(options) { }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Ensure salary cannot be negative or zer
            modelBuilder.Entity<Employee>()
                .HasCheckConstraint("CK_Employee_Salary", "Salary > 0");


        }
    }

}

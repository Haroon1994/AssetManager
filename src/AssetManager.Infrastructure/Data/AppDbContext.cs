namespace AssetManager.Infrastructure.Data;

using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Assignment> Assignments => Set<Assignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>()
            .HasIndex(d => d.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Device)
            .WithMany(d => d.Assignments)
            .HasForeignKey(a => a.DeviceId);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Assignments)
            .HasForeignKey(a => a.EmployeeId);
    }
}

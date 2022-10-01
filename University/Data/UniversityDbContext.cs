using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Data;
public class UniversityDbContext : DbContext {
	public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
    { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Teacher>()
        //     .HasOne(b => b.Address)
        //     .WithOne(i => i.Teacher)
        //     .HasForeignKey<Address>(b => b.TeacherId);
    }
}
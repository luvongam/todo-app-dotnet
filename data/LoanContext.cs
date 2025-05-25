using LoanApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Data
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions<LoanContext> options) : base(options) { }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .Property(l => l.ApplicationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Loan>()
                .Property(l => l.Status)
                .HasDefaultValue("Pending");
        }
    }
    
}
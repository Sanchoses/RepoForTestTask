using Microsoft.EntityFrameworkCore;
using PostgreTest.Data.Entities;

namespace PostgreTest.Data;
public class TransactionsDbContext: DbContext {

	public DbSet<Product_Transactions> Product_Transactions { get; set; }
    public DbSet<LegalAddress> LegalAddresses { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Transactions> Transactions { get; set; }
    

    public TransactionsDbContext(DbContextOptions<TransactionsDbContext> opt)
    :base(opt)
    {
        Database.EnsureCreated();
    }
}
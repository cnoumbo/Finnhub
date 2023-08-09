using System;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class StockMarketDbContext : DbContext
{
	public DbSet<BuyOrder> BuyOrders { get; set; }
	public DbSet<SellOrder> SellOrders { get; set; }

	public StockMarketDbContext(DbContextOptions options) : base(options)
	{

	}

    // Configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure database table names
        modelBuilder.Entity<BuyOrder>().ToTable("buyorders");
        modelBuilder.Entity<SellOrder>().ToTable("sellorders");

        // nothing to seed
    }
}


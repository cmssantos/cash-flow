using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=10.0.0.10;Database=cashflow;Uid=root;Pwd=@#123abc";
        var serverVersion = new MySqlServerVersion(new Version(major: 8, minor: 4, build: 4));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}

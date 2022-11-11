using Microsoft.EntityFrameworkCore;
using QuoteApplication.Models;

namespace QuoteApplication.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Quote> Quotes { get; set; }
}

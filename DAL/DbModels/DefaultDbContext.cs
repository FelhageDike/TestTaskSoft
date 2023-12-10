using Microsoft.EntityFrameworkCore;

namespace DAL.DbModels;

public class DefaultDbContext : DbContext
{
	
	public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
		: base(options)
	{
	}

	public DbSet<TestModel> Tests { get; set; }
}
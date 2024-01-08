using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperation
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
		{

		}

		public DbSet<Book> Books { get; set; }
	}
}

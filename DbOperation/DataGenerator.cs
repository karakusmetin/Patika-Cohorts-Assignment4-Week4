using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperation
{
	public class DataGenerator
	{
		public static void Initilize(IServiceProvider serviceProvider)
		{
			using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			{
				if (context.Books.Any())
				{
					return;
				}

				context.Books.AddRange
				(
					new Book()
					{
						Title = "LeanStartup",
						GenreId = 1, // Personel Growth	
						PageCount = 200,
						PublisDate = new DateTime(2001, 06, 12),
					},
					new Book()
					{
						Title = "Herland",
						GenreId = 2, // Science Fiction	
						PageCount = 250,
						PublisDate = new DateTime(2010, 05, 23),
					},
					new Book()
					{
						Title = "Dune",
						GenreId = 2, // Personel Growth	
						PageCount = 540,
						PublisDate = new DateTime(2002, 05, 22),
					}
				);

				context.SaveChanges();
			}
		}
	}
}

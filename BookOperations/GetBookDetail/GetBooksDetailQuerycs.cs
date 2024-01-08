using WebApi.Common;
using WebApi.DbOperation;

namespace WebApi.BookOperations.GetBookDetail
{
	public class GetBooksDetailQuery
	{
		private readonly BookStoreDbContext _dbContext;
		

		public int BookID {  get; set; }
		public GetBooksDetailQuery(BookStoreDbContext dbContext) 
		{
			_dbContext = dbContext;

		}
		public BookDetailViewModel Handle()
		{
			var book = _dbContext.Books.Where(book => book.Id == BookID).SingleOrDefault();
			if (book == null)
			{
				throw new InvalidOperationException("Kitap Bulunamadı");
			}
			BookDetailViewModel vm = new BookDetailViewModel();
			vm.Title = book.Title;
			vm.PageCount = book.PageCount;
			vm.PublishDate = book.PublisDate.Date.ToString("dd/MM/yyyy");
			vm.Genre = ((GenreEnum)book.GenreId).ToString();
			return vm;
		}

	}

	public class BookDetailViewModel
	{
        public string Title { get; set; }
		public string Genre { get; set; }
        public int PageCount { get; set; }
		public string PublishDate {  get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperation;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
	[Route("[controller]s")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly BookStoreDbContext context;

		
		
		public BookController(BookStoreDbContext dbContext)
		{
			context = dbContext;
		}

		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(context);
			var result = query.Handle();
			return Ok(result);

		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			BookDetailViewModel result;
			GetBooksDetailQuery query = new GetBooksDetailQuery(context);
			try
			{
				query.BookID = id;
				result = query.Handle();
			}

			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(result);
		}

		//[HttpGet]
		//public Book Get([FromQuery]string id)
		//{
		//	var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
		//	return book;
		//}

		[HttpPost]
		public IActionResult AddBook([FromBody] CreateBookModel newBook)
		{
			CreateBookCommand command = new CreateBookCommand(context);
			try
			{
				command.Model = newBook;
				command.Handle();

			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}

	
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
		{
			try
			{
				UpdateBookCommand command = new UpdateBookCommand(context);
				command.BookId = id;
				command.Model = updatedBook;
				command.Handle();
		}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{
			try
			{
				DeleteBookCommand command = new DeleteBookCommand(context);
				command.BookId = id;
				command.Handle();
			}
			catch(Exception ex) 
			{
				return BadRequest(ex.Message);	
			}
		
			return Ok();
		}

	}
}



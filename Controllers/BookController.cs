using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
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

		private readonly IMapper mapper;
		
		public BookController(BookStoreDbContext dbContext, IMapper _mapper)
		{
			context = dbContext;
			mapper = _mapper;
		}

		//private static List<Book> BookList = new List<Book>()
		//{
		//	new Book()
		//	{
		//		Id = 1,
		//		Title = "LeanStartup",
		//		GenreId = 1, // Personel Growth	
		//		PageCount = 200,
		//		PublisDate = new DateTime(2001,06,12),
		//	},
		//	new Book()
		//	{
		//		Id = 2,
		//		Title = "Herland",
		//		GenreId = 2, // Science Fiction	
		//		PageCount = 250,
		//		PublisDate = new DateTime(2010,05,23),
		//	},
		//	new Book()
		//	{
		//		Id = 3,
		//		Title = "Dune",
		//		GenreId = 2, // Personel Growth	
		//		PageCount = 540,
		//		PublisDate = new DateTime(2002,05,22),
		//	}

		//};

		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(context, mapper);
			var result = query.Handle();
			return Ok(result);

		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			BookDetailViewModel result;
			GetBooksDetailQuery query = new GetBooksDetailQuery(context,mapper);
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
			CreateBookCommand command = new CreateBookCommand(context, mapper);
			try
			{
				command.Model = newBook;
				CreateBookCommandValidator validator = new CreateBookCommandValidator();
				ValidationResult result = validator.Validate(command);
				validator.ValidateAndThrow(command);
				//if(result.IsValid) 
				//{
				//	foreach(var item in result.Errors)
				//	{
				//		Console.WriteLine("Özellik" + item.PropertyName + " - Error Message " + item.ErrorMessage);
				//	}
				//}
				//else
				//	command.Handle();	
				
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
				DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
				validator.ValidateAndThrow(command);
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



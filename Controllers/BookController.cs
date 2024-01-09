﻿using AutoMapper;
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
			
			try
			{
				GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
				query.BookID = id;
				GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
				validator.ValidateAndThrow(query);
				result = query.Handle();
			}

			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(result);
		}

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
				UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
				ValidationResult result = validator.Validate(command);
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



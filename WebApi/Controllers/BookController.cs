using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase{
        //sadece burda kullanacağım bir instance oluşturdum.
        private readonly BookStoreDbContext _context;
        //constructor aracılığı ile inject edilen dbcontext alavilirim:
        public BookController(BookStoreDbContext context){
             _context = context;//private olan _contexti inject edilen instance atadım.
        }//file içerisinde değiştirilmesini istemiyorum. readonly sadece constructor içerisinde set edilebilir.

        [HttpGet]
        public IActionResult GetBooks(){
           GetBooksQuery query = new GetBooksQuery(_context);
           var result = query.Handle();
           return Ok(result);
        } 

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
            return Ok(result);
        } 

        // [HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = BookList.Where(book=>book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook; 
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            //exception ı kullanıcıya geridönmeliyim.yoksa kod kırılır. 
            //o yüzden try-catch kullanalım.
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook){
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
           
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id){
            
            var book = _context.Books .SingleOrDefault(x=>x.Id==id);
            if(book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
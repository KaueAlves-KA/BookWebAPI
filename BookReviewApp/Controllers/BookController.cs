// Por algum motivo, usar este -> using Microsoft.AspNetCore.Components;
// vai causar erros, então devemos usar este -> using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers
{
    // Esses atributos vão tornar seus Controllers de fato em Controllers
    // se não definirmos tudo corretamente será somente uma classe chamada Controller
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, 
            IOwnerRepository ownerRepository, 
            IReviewRepository reviewRepository, 
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _ownerRepository = ownerRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        // Nesse Endpoint vamos produzir uma resposta de código 200
        // "OK", para cada livro que for enumerado pelo IEnumerable
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            // Sempre prestar atenção na convenção dos nomes, acima estamos nomeando GetBooks
            // com S pois estamos chamando uma lista de livros

            // Ao utilizar o automapper não precisaremos criar todo o objeto manualmente
            // e atribuir os valores aos seus atributos, o automapper faz isso automaticamente para nós
            // exemplo -> var new Book {
            //                      id = 2
            //                      Title = "algo" 
            //                    }
            // somente um exemplo para entender o que o automapper está fazendo
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            } 
            else
            {
                return Ok(books);
            }
        }
        
        // Endpoint para buscar um livro com base em seu Id
        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook(int bookId) 
        {
            // Já aqui estamos recuperando um Book somente então não nomeamos no plural
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var books = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else 
            {
                return Ok(books);
            }
        }

        [HttpGet("rating/{bookId}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetBookRating(int bookId)
        {
            if (!_bookRepository.BookExists(bookId)) 
            {
                return NotFound();
            }
            var rating = _bookRepository.GetBookRating(bookId);
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            else 
            {
                return Ok(rating);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBook([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] BookDto bookCreate)
        {
            if (bookCreate == null)
            {
                return BadRequest(ModelState);
            }
            var books = _bookRepository.GetBooks()
                .Where(c => c.Title.Trim().ToUpper() == bookCreate.Title.Trim().ToUpper())
                .FirstOrDefault();
            if (books != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var bookMap = _mapper.Map<Book>(bookCreate);
                        
            if (!_bookRepository.CreateBook(ownerId, categoryId, bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBook(int bookId, 
            [FromQuery] int ownerId, 
            [FromQuery] int categoryId, 
            [FromBody] BookDto updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest(ModelState);
            }
            if (bookId != updatedBook.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }
            var bookMap = _mapper.Map<Book>(updatedBook);

            if (!_bookRepository.UpdateBook(ownerId, categoryId, bookMap))
            {
                ModelState.AddModelError("", "Something went wrong updating book");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteBook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }
            var reviewsToDelete = _reviewRepository.GetReviewOfABook(bookId);
            var bookToDelete = _bookRepository.GetBook(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList())) 
            {
                ModelState.AddModelError("", "Something wnet wrong when deleting reviews");
            }
            if (!_bookRepository.DeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting book");
            }

            return NoContent();
        }
    }
}

using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

// No RepositoryPattern nós colocaremos somente chamadas de banco de dados
// se você tiver coisas que não são chamadas de banco de dados aqui, isso serão
// serviços (services), se você algum dia chegar a ver uma pasta chamada service
// são pedaços de código que estão sendo chamados e utilizados também, mas
// não são chamadas de banco de dados.

namespace BookReviewApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context) 
        {
            _context = context;
        }

        // Verifica se um Book existe com base no seu Id
        public bool BookExists(int bookId)
        {
            // Podemos fazer a verificação da exitência de algo em qualquer lugar
            // porém faremos aqui para podermos repetir posteriormente justamente
            // porque estamos usando o Repository Pattern para facilitar o uso
            // de métodos assim.
            return _context.Book.Any(b => b.Id == bookId);
        }

        // Criar livros
        public bool CreateBook(int ownerId, int categoryId, Book book)
        {
            var bookOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var bookOwner = new BookOwner()
            {
                Owner = bookOwnerEntity,
                Book = book
            };
            _context.Add(bookOwner);

            var bookCategory = new BookCategory()
            {
                Category = category,
                Book = book
            };

            _context.Add(bookCategory);

            _context.Add(book);

            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _context.Remove(book);
            return Save();
        }


        // Chamar registros de livro com base no Id
        public Book GetBook(int id)
        {
            return _context.Book.Where(b => b.Id == id).FirstOrDefault();
        }

        // Chamar registros de livro com base no Title
        public Book GetBook(string title)
        {
            return _context.Book.Where(b => b.Title == title).FirstOrDefault();
        }

        // Chamar a média do rating dos livros
        public decimal GetBookRating(int bookId)
        {
            var review = _context.Reviews.Where(b => b.Book.Id == bookId);
            if (review.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return (decimal)review.Sum(r => r.Rating) / review.Count();
            }
        }

        // Chamar lista de livros
        public ICollection<Book> GetBooks()
        {
            // Estamos retornando uma LISTA de livros ordenados pelo ID
            // (ICollection<>) então precisamos colocar ToList() no final
            return _context.Book.OrderBy(b => b.Id).ToList();
        }

        // Salvar mudanças
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // Atualizar livros
        public bool UpdateBook(int ownerId, int categoryId, Book book)
        {
            _context.Update(book);
            return Save();
        }
    }
}

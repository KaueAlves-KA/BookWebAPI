using BookReviewApp.Models;

namespace BookReviewApp.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(int id);
        Book GetBook(string title);
        decimal GetBookRating(int bookId);
        bool BookExists(int bookId);
        bool CreateBook(int ownerId, int categoryId, Book book);
        bool UpdateBook(int ownerId, int categoryId, Book book);
        bool DeleteBook(Book book);
        bool Save();
    }
}

using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            // Change tracker
            // add,updating,modifying 
            // conected - maior parte do tempo vai ser usado, disconected
            // EntityState.Added -> provavelmente disconected
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Book> GetBookByCategory(int categoryId)
        {
            // Quanto temos entidades aninhadas / propriedades de navegação
            // o Entity framework não vai carregar isso automaticamente 
            // precisaremos dar um Select ou um Include para trazer isso
            return _context.BookCategories.Where(e => e.CategoryId == categoryId).Select(b => b.Book).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            // Se somente estamos retornando um objeto ent colocamos FirstOrDefault
            // se for um ICollection retornamos .ToList() por se tratar de uma lista.
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}

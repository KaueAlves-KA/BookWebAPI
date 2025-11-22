using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApp.Data
{
    public class DataContext : DbContext
    {
        // Construtor do DataContext que puxará os dados do banco de dados
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        // Dando contexto das tabelas recuperadas à nossa classe do Context
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country>  Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookOwner> BookOwners { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esse método modifica a tabela via código pois em alguns cenários como nesse
            // é mais vantajoso que seja feito por código ao invés de modificar diretamente
            // no banco de dados.
            // Além de que as demais pessoas no projeto saberão quais foram as mudanças
            // aplicadas.

            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>()
                .HasOne(b => b.Book)
                .WithMany(bc => bc.BookCategories)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookCategory>()
                .HasOne(b => b.Category)
                .WithMany(bc => bc.BookCategories)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<BookOwner>()
                .HasKey(bo => new { bo.BookId, bo.OwnerId });
            modelBuilder.Entity<BookOwner>()
                .HasOne(b => b.Book)
                .WithMany(bo => bo.BookOwners)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookOwner>()
                .HasOne(b => b.Owner)
                .WithMany(bc => bc.BookOwners)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}

namespace BookReviewApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookOwner> BookOwners { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}

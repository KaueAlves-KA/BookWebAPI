namespace BookReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public Country Country { get; set; }
        public ICollection<BookOwner> BookOwners { get; set; }
    }
}

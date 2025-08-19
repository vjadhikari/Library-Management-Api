namespace LibraryManagement.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<Borrow>? Borrows { get; set; }
    }
}

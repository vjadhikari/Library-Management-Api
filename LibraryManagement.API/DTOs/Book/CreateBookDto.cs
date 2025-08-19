namespace LibraryManagement.API.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
    }
}

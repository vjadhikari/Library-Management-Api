namespace LibraryManagement.API.DTOs.Borrow
{
    public class BorrowCreateDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}

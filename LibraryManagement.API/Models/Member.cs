namespace LibraryManagement.API.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Borrow>? Borrows { get; set; }
    }
}

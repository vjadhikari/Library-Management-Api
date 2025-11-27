using Entities.Models;

namespace RepositoryContract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Author> AuthorRepository { get; }
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<Borrow> BorrowRepository {  get; }
        IGenericRepository<Member> MemberRepository { get; }
        IGenericGetRepository<Author> AuthorGetRepository { get; }
        IGenericGetRepository<Book> BookGetRepository { get; }

        Task<int> SaveChangesAsync();
    }
}

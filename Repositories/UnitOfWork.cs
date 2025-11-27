using Entities.Data;
using Entities.Models;
using RepositoryContract;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        private IGenericRepository<Author> _authorRepository;
        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<Borrow> _borrowRepository;
        private IGenericRepository<Member> _memberRepository;
        private IGenericGetRepository<Author> _authorGetRepository;
        private IGenericGetRepository<Book> _bookGetRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Author> AuthorRepository
        {
            get
            {
                return _authorRepository ??= new GenericRepository<Author>(_context);
            }
        }

        public IGenericRepository<Book> BookRepository
        {
            get
            {
                return _bookRepository ??= new GenericRepository<Book>(_context);
            }
        }

        public IGenericRepository<Borrow> BorrowRepository
        {
            get
            {
                return _borrowRepository ??= new GenericRepository<Borrow>(_context);
            }
        }

        public IGenericRepository<Member> MemberRepository
        {
            get
            {
                return _memberRepository ??= new GenericRepository<Member>(_context);
            }
        }

        public IGenericGetRepository<Author> AuthorGetRepository
        {
            get
            {
                return _authorGetRepository ??= new GenericGetRepository<Author>(_context);
            }
        }
        public IGenericGetRepository<Book> BookGetRepository
        {
            get
            {
                return _bookGetRepository ??= new GenericGetRepository<Book>(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

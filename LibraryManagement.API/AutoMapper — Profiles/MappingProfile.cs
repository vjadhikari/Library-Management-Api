using AutoMapper;
using Entities.Models;
using ServiceContract.DTOs.Author;
using ServiceContract.DTOs.Book;
using ServiceContract.DTOs.Borrow;
using ServiceContract.DTOs.Member;


namespace LibraryManagement.API.AutoMapper___Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();
            CreateMap<Author, UpdateAuthorDto>().ReverseMap();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookDto, Book>();

            CreateMap<Borrow, BorrowReadDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));

            CreateMap<BorrowCreateDto, Borrow>();
            CreateMap<BorrowUpdateDto, Borrow>();

            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();

        }
    }
}

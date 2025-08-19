using AutoMapper;
using LibraryManagement.API.DTOs.Author;
using LibraryManagement.API.DTOs.Book;
using LibraryManagement.API.DTOs.Borrow;
using LibraryManagement.API.DTOs.Member;
using LibraryManagement.API.Models;

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

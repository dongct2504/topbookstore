using AutoMapper;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();

        CreateMap<Publisher, PublisherDto>();
        CreateMap<PublisherDto, Publisher>();

        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
            .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(
                src => src.Categories.Select(c => c.CategoryId).ToArray()));
        CreateMap<BookDto, Book>();
    }
}
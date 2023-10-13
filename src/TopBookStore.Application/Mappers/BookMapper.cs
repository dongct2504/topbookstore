using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Mappers;

public static class BookMapper
{
    public static BookDTO MapToDTO(Book book)
    {
        return new BookDTO
        {
            BookId = book.BookId,
            Title = book.Title,
            Description = book.Description,
            Isbn13 = book.Isbn13,
            Inventory = book.Inventory,
            Price = book.Price,
            DiscountPercent = book.DiscountPercent,
            NumberOfPages = book.NumberOfPages,
            PublicationDate = book.PublicationDate,
            ImageUrl = book.ImageUrl,
            AuthorId = book.AuthorId,
            PublisherId = book.PublisherId,
            CategoryIds = book.Categories.Select(c => c.CategoryId).ToArray(),
            AuthorName = book.Author.FullName,
            PublisherName = book.Publisher.Name
        };
    }

    public static Book MapToEntity(BookDTO bookDTO)
    {
        return new Book
        {
            BookId = bookDTO.BookId,
            Title = bookDTO.Title,
            Description = bookDTO.Description,
            Isbn13 = bookDTO.Isbn13,
            Inventory = bookDTO.Inventory,
            Price = bookDTO.Price,
            DiscountPercent = bookDTO.DiscountPercent,
            NumberOfPages = bookDTO.NumberOfPages,
            PublicationDate = bookDTO.PublicationDate,
            ImageUrl = bookDTO.ImageUrl,
            AuthorId = bookDTO.AuthorId,
            PublisherId = bookDTO.PublisherId
        };
    }
}
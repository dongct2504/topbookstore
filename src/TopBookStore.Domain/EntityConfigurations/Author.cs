using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Domain.Entities;

[MetadataType(typeof(AuthorMetadata))]
public partial class Author
{
    public string FullName => $"{LastName} {FirstName}";
}

public class AuthorMetadata
{
    
}
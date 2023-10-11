using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Categories")]
[MetadataType(typeof(CategoryMetadata))]
public partial class Category
{

}

public class CategoryMetadata
{

}
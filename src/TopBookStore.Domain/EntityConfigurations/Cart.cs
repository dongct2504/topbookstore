using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TopBookStore.Domain.Entities;

[MetadataType(typeof(CartMetaData))]
public partial class Cart
{
    [JsonIgnore]
    public decimal SubTotal => Book.Price * Quantity;
}

public class CartMetaData
{

}
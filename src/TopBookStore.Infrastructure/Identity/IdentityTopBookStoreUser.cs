using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Infrastructure.Identity;

// Add profile data for application users by adding properties to the IdentityTopBookStoreUser class
public class IdentityTopBookStoreUser : IdentityUser
{
    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    [NotMapped]
    public string Role { get; set; } = string.Empty;
}

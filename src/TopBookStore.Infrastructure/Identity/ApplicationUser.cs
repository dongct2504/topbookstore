using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Infrastructure.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public int CustomerId { get; set; }  // Foreign key property
    public virtual Customer Customer { get; set; } = null!;  // Navigation property
}


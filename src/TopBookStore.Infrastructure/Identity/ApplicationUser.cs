using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Infrastructure.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;
}


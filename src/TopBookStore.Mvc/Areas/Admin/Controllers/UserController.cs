using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Constants;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleConstants.RoleAdmin)]
public class UserController : Controller
{
    private readonly TopBookStoreContext _context;

    public UserController(TopBookStoreContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region API CALLS

    public async Task<IActionResult> GetAllUsers()
    {
        List<IdentityTopBookStoreUser> users = await _context.Users
            .Include(u => u.Customer)
            .ToListAsync();

        // this will be the mapping between the users and the roles
        List<IdentityUserRole<string>> userRoles = await _context.UserRoles.ToListAsync();

        List<IdentityRole> roles = await _context.Roles.ToListAsync();

        // maping users
        List<UserDto> userDtos = new();

        foreach (IdentityTopBookStoreUser user in users)
        {
            string roleId = userRoles.FirstOrDefault(ur => ur.UserId == user.Id)?.RoleId ?? string.Empty;
            // add role for the user in users
            user.Role = roles.FirstOrDefault(r => r.Id == roleId)?.Name ?? string.Empty;

            UserDto userDto = new()
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Role = user.Role,
                LockoutEnd = user.LockoutEnd?.UtcDateTime ?? null,
                FirstName = user.Customer.FirstName,
                LastName = user.Customer.LastName,
            };
            userDtos.Add(userDto);
        }

        return Json(new { data = userDtos });
    }

    [HttpGet]
    public async Task<IActionResult> LockUnlockUser(string id)
    {
        IdentityTopBookStoreUser? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            return Json(new { success = false, message = "Không tìm thấy dữ liệu." });
        }

        DateTime currentDate = DateTime.Today;
        DateTime lockoutEndDate = user.LockoutEnd?.UtcDateTime ?? new DateTime(1000, 1, 1);

        if (lockoutEndDate > currentDate)
        {
            // unlock it
            user.LockoutEnd = currentDate.AddYears(-100);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã mở khóa." });
        }
        else
        {
            // lock it
            user.LockoutEnd = currentDate.AddYears(100);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã khóa." });
        }
    }

    #endregion
}
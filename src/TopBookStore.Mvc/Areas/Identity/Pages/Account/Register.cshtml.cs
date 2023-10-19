// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Infrastructure.Identity;

namespace TopBookStore.Mvc.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityTopBookStoreUser> _signInManager;
        private readonly UserManager<IdentityTopBookStoreUser> _userManager;
        private readonly IUserStore<IdentityTopBookStoreUser> _userStore;
        private readonly IUserEmailStore<IdentityTopBookStoreUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICustomerService _service;

        public RegisterModel(
            UserManager<IdentityTopBookStoreUser> userManager,
            IUserStore<IdentityTopBookStoreUser> userStore,
            SignInManager<IdentityTopBookStoreUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ICustomerService service)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _service = service;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Vui lòng nhập email.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
            [StringLength(100, ErrorMessage =
                "{0} phải có ít nhất {2} ký tự và nhiều nhất {1} kí tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập tên.")]
            [StringLength(80, ErrorMessage = "Nhập Tên ngắn hơn 80 kí tự.")]
            public string FirstName { get; set; } = null!;

            [Required(ErrorMessage = "Vui lòng nhập họ.")]
            [StringLength(80, ErrorMessage = "Nhập Tên ngắn hơn 80 kí tự.")]
            public string LastName { get; set; } = null!;

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
            [StringLength(15, ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ.")]
            public string PhoneNumber { get; set; } = null!;

            public string Role { get; set; } = string.Empty;

            public List<SelectListItem> RoleList { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            Input = new InputModel
            {
                RoleList = await _roleManager.Roles
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Name
                    })
                    .ToListAsync()
            };

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // var user = CreateUser();

                Customer customer = new()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                await _service.AddCustomerAsync(customer);

                IdentityTopBookStoreUser user = new()
                {
                    Email = Input.Email,
                    UserName = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    Role = Input.Role,
                    CustomerId = customer.CustomerId,
                };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (!await _roleManager.RoleExistsAsync(RoleConstants.RoleCustomer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.RoleCustomer));
                    }
                    if (!await _roleManager.RoleExistsAsync(RoleConstants.RoleLibrarian))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.RoleLibrarian));
                    }
                    if (!await _roleManager.RoleExistsAsync(RoleConstants.RoleAdmin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.RoleAdmin));
                    }

                    // Assign role for user, only for testing
                    if (customer.FirstName == "Dong" || customer.FirstName == "Duc" ||
                        customer.FirstName == "Duy" || customer.FirstName == "Giap" ||
                        customer.FirstName == "Diep")
                    {
                        await _userManager.AddToRoleAsync(user, RoleConstants.RoleAdmin);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.Role))
                        {
                            await _userManager.AddToRoleAsync(user, RoleConstants.RoleCustomer);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, user.Role);
                        }
                    }

                    // Generate email confirmation, again only for testing
                    if (customer.FirstName != "Dong" || customer.FirstName != "Duc" ||
                        customer.FirstName != "Duy" || customer.FirstName != "Giap" ||
                        customer.FirstName != "Diep")
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new
                            {
                                area = "Identity",
                                // these are use member name
                                userId,
                                code,
                                returnUrl
                            },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation",
                            new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.Role)) // for customer
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else // for admin register a new user
                        {
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }
                    }
                }
                else
                {
                    await _service.RemoveCustomerAsync(customer);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            Input = new InputModel
            {
                RoleList = await _roleManager.Roles
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Name
                    })
                    .ToListAsync()
            };

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityTopBookStoreUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityTopBookStoreUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityTopBookStoreUser)}'. " +
                    $"Ensure that '{nameof(IdentityTopBookStoreUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityTopBookStoreUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityTopBookStoreUser>)_userStore;
        }
    }
}

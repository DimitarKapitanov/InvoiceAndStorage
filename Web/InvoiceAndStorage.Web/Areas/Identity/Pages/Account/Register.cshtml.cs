namespace InvoiceAndStorage.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Data.Models;
    using InvoiceAndStorage.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly ICompanyServise companyServise;
        private readonly IDataBaseOwnerService createDataBaseOwnerService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICompanyServise companyServise,
            IDataBaseOwnerService createDataBaseOwnerService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.companyServise = companyServise;
            this.createDataBaseOwnerService = createDataBaseOwnerService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Потребителско име")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде с дължина между {2} и {1}", MinimumLength = 3)]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Телефонен номер")]
            [StringLength(10, ErrorMessage = "Невалиден телефонен номер", MinimumLength = 10)]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде с дължина между {2} и {1}", MinimumLength = 3)]
            [Display(Name = "Име")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Фамилия")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде с дължина между {2} и {1}", MinimumLength = 3)]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [DataType(DataType.Date)]
            [Display(Name = "Дата на раждане")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Парола")]
            [DataType(DataType.Password, ErrorMessage = "{0} е задължителна")]
            [RegularExpression("^[a-zA-Z0-9]{6,20}$", ErrorMessage = "{0} трябва да съдържа само букви и цифри с дължина между шест и двадесет!")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [DataType(DataType.Password)]
            [Display(Name = "Портвърди паролата")]
            [Compare("Password", ErrorMessage = "Паролата и потвърждението на паролата трябва да съвпадат.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "ЕИК на фирмата")]
            [RegularExpression("^([0-9]{9}|[0-9]{12})$", ErrorMessage = "{0} трябва да бъде между девет и дванадесет цифри")]
            public string CompanyIdentificationNumber { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Име на фирмата")]
            [StringLength(100, ErrorMessage = "{0} трябва да е между {2} и {1} символа", MinimumLength = 2)]
            public string CompanyName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Материално отговорно лице (Име и фамилия)")]
            [RegularExpression(@"^[А-Я]{1}[а-я]{2,20}\s[А-Я]{1}[а-я]{2,20}$", ErrorMessage = "Дължината на името или фамилията трябва да е минимум два и максимум двадесет символа започващ и с главна буква")]
            public string CompanyOwner { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Име на банката")]
            public string BankName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Банкова сметка")]
            [RegularExpression("^[BG]{2}[0-9]{2}[A-Z]{4}[0-9]{14}$", ErrorMessage = "Невалидна банкова сметка. Сметката трябва да започва с BG две цифри четири латински главни букви следвани от четиринадесет цифри от нула до девет")]
            public string BankAccount { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Банков код")]
            public string BankCode { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Име на държават по регистраця на фирмата")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
            public string CountryName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Град по регистрация на фирмата")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
            public string CityName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Улица по регистрация на фирмата")]
            [StringLength(30, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа", MinimumLength = 3)]
            public string StreetName { get; set; }

            [Required(ErrorMessage = "Полето {0} е задължително")]
            [Display(Name = "Номер на улицата по регистрация на фирмата")]
            [Range(0, int.MaxValue, ErrorMessage = "{0} трябва да бъде между {2} и {1}")]
            public int StreetNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var companyId = await this.companyServise
                    .CreateCompany(
                        this.Input.CompanyName,
                        this.Input.CompanyOwner,
                        this.Input.CompanyIdentificationNumber,
                        this.Input.BankAccount,
                        this.Input.BankCode,
                        this.Input.CountryName,
                        this.Input.CityName,
                        this.Input.StreetName,
                        this.Input.StreetNumber,
                        this.Input.BankName);

                var user = new ApplicationUser
                {
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    BirthDate = this.Input.BirthDate,
                    PhoneNumber = this.Input.PhoneNumber,
                    UserName = this.Input.UserName,
                    Email = this.Input.Email,
                    DatabaseОwnerId = await this.createDataBaseOwnerService.
                    CreateDataBaseOwner(companyId),
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    await this.createDataBaseOwnerService.AddUser(user, user.DatabaseОwnerId);

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}

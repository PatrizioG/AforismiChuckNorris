using ChuckNorrisAphorisms.Data.Entities;
using ChuckNorrisAphorisms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Areas.Identity.Pages.Account
{
    [Authorize]
    public class CreateAphorismModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAphorismsService _aphorismsService;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly EmailSenderOptions _emailSenderOptions;

        public class InputModel
        {
            [Required]
            public string Aphorism { get; set; }

            [Required]
            public string Culture { get; set; }
        }

        public List<SelectListItem> Cultures
        {
            get
            {
                var cultures = new List<SelectListItem>();

                foreach (var culture in _configuration.GetSection("SupportedCultures").GetChildren())
                {
                    cultures.Add(new SelectListItem
                    {
                        Value = culture.GetSection("Value").Value,
                        Text = culture.GetSection("Text").Value
                    });
                }
                return cultures;
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public CreateAphorismModel(
            UserManager<ApplicationUser> userManager,
            IAphorismsService aphorismsService,
            IConfiguration configuration,
            IEmailSender emailSender,
            IOptions<EmailSenderOptions> emailSenderOptions)
        {
            _userManager = userManager;
            _aphorismsService = aphorismsService;
            _configuration = configuration;
            _emailSender = emailSender;
            _emailSenderOptions = emailSenderOptions.Value;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && !Input.Aphorism.ToLower().Contains("chuck norris"))
            {
                ModelState.AddModelError("Input.Aphorism", "Aphorism must contains \"Chuck Norris\"");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Avviso amministratore che un utente ha inserito una richiesta di aforisma
            await _emailSender.SendEmailAsync(_emailSenderOptions.AdministratorEmail, "New aphorism pending request",
            $"The user {user.UserName} send this aphorism:{Input.Aphorism}, to accept click here: {Url.Page("/ManagePendingAphorisms")}");

            await _aphorismsService.AddAphorism(Input.Aphorism, Input.Culture, user.Id, true, Data.Models.AphorismStatus.Pending);

            return RedirectToPage("ManageAphorisms");
        }
    }
}

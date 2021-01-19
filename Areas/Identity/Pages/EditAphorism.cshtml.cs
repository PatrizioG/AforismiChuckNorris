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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Areas.Identity.Pages
{
    [Authorize]
    public class EditAphorismModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAphorismsService _aphorismsService;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly EmailSenderOptions _emailSenderOptions;

        public class EditModel
        {
            [Required]
            public string Aphorism { get; set; }

            [Required]
            public string Culture { get; set; }

            public Guid Id { get; set; }
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
        public EditModel Input { get; set; } = new EditModel();

        public EditAphorismModel(
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


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var aphorism = await _aphorismsService.GetAphorism(id);

            if (aphorism == null)
                return NotFound($"Unable to load aphorism with ID '{id}'.");

            Input.Aphorism = aphorism.Value;
            Input.Culture = aphorism.Culture;
            Input.Id = aphorism.Id;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.Id != aphorism.UserId)
                return Forbid();


            return Page();
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

            var updatedAphorism = await _aphorismsService.GetAphorism(Input.Id);

            if (updatedAphorism == null)
                return NotFound($"Unable to load aphorism with ID '{Input.Id}'.");

            updatedAphorism.Value = Input.Aphorism;
            updatedAphorism.Culture = Input.Culture;
            updatedAphorism.UpdateDate = DateTime.UtcNow;

            await _aphorismsService.EditAphorism(updatedAphorism);

            return RedirectToPage("ManageAphorisms");
        }
    }
}

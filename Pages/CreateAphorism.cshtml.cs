using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AforismiChuckNorris.Data.Entities;
using AforismiChuckNorris.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AforismiChuckNorris.Areas.Identity.Pages.Account
{
    [Authorize]
    public class CreateAphorismModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAphorismsService _aphorismsService;

        public class InputModel
        {
            [Required]
            public string Aphorism { get; set; }

            [Required]
            public string Culture { get; set; }
        }

        public List<SelectListItem> Cultures { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "it-IT", Text = "Italian" },
            new SelectListItem { Value = "en-EN", Text = "English" },
        };

        [BindProperty]
        public InputModel Input { get; set; }

        public CreateAphorismModel(
            UserManager<ApplicationUser> userManager,
            IAphorismsService aphorismsService)
        {
            _userManager = userManager;
            _aphorismsService = aphorismsService;
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

            // todo sobstitute chuck norris with {0}
            await _aphorismsService.AddAphorism(Input.Aphorism, Input.Culture, user.Id, true, Data.Models.AphorismStatus.Pending);

            return RedirectToPage("ManageAphorisms");
        }
    }
}

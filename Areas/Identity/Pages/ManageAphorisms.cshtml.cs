using ChuckNorrisAphorisms.Data.Entities;
using ChuckNorrisAphorisms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ManageAphorismsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAphorismsService _aphorismsService;

        public ManageAphorismsModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAphorismsService aphorismsService)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _aphorismsService = aphorismsService;
        }

        public IEnumerable<Aphorism> PendingAphorisms { get; set; } = new List<Aphorism>();
        public IEnumerable<Aphorism> PublishedAphorisms { get; set; } = new List<Aphorism>();

        public bool CanAddAphorisms { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser = await _userManager.GetUserAsync(User);
            if (ApplicationUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // preleva tutti gli aforismi dell'utente
            var aphorisms = _aphorismsService.GetAphorismsOwnedBy(ApplicationUser.Id);

            PendingAphorisms = aphorisms.Where(a => a.Status == Data.Models.AphorismStatus.Pending);
            PublishedAphorisms = aphorisms.Where(a => a.Status == Data.Models.AphorismStatus.Published);

            if (!ApplicationUser.MaxPendingRequest.HasValue)
            {
                // l'utente non ha limite di aggiunta
                CanAddAphorisms = true;
            }
            else if (PublishedAphorisms.Count() >= ApplicationUser.MaxPendingRequest.Value)
                CanAddAphorisms = false;
            else
                CanAddAphorisms = true;

            return Page();
        }
    }
}

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
        private readonly IAphorismsService _aphorismsService;

        public ManageAphorismsModel(IAphorismsService aphorismsService)
        {
            _aphorismsService = aphorismsService;
        }

        public IEnumerable<Aphorism> PendingAphorisms { get; set; } = new List<Aphorism>();
        public IEnumerable<Aphorism> PublishedAphorisms { get; set; } = new List<Aphorism>();

        public bool CanAddAphorisms { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}

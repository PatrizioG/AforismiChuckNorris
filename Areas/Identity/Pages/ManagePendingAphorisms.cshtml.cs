using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuckNorrisAphorisms.Data.Entities;
using ChuckNorrisAphorisms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChuckNorrisAphorisms.Pages
{
    [Authorize(Roles = "Administrator")]
    public class ManagePendingAphorismsModel : PageModel
    {
        private readonly IAphorismsService _aphorismsService;

        public IEnumerable<Aphorism> PendingAphorisms { get; set; } = new List<Aphorism>();

        public ManagePendingAphorismsModel(IAphorismsService aphorismsService)
        {
            _aphorismsService = aphorismsService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            PendingAphorisms = await _aphorismsService.GetPendingAphorism();

            return Page();
        }
    }
}

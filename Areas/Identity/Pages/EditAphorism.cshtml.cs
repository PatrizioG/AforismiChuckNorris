using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AforismiChuckNorris.Areas.Identity.Pages
{
    [Authorize]
    public class EditAphorismModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            return Page();
        }
    }
}

using AforismiChuckNorris.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AforismiChuckNorris.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAphorismsService _aphorismsService;
        public int AphorismsCount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAphorismsService aphorismsService)
        {
            _logger = logger;
            _aphorismsService = aphorismsService;
        }

        public void OnGet()
        {
            AphorismsCount = _aphorismsService.GetAphorismCount();
        }
    }
}

using AforismiChuckNorris.Data;
using AforismiChuckNorris.Data.Models;
using AforismiChuckNorris.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Controllers
{
    [ApiController]
    [Route("api/aphorism")]
    public class AphorismsController : ControllerBase
    {
        private readonly ILogger<AphorismsController> _logger;
        private readonly IAphorismsService _aphorismsService;
        private readonly IMapper _mapper;

        public AphorismsController(
            ILogger<AphorismsController> logger,
            IAphorismsService aphorismsService, 
            IMapper mapper)
        {
            _logger = logger;
            _aphorismsService = aphorismsService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAphorism(Guid id, string subject)
        {
            var aphorism = await _aphorismsService.GetAphorism(id);

            if (aphorism != null)
            {
                if (!string.IsNullOrEmpty(subject))
                    aphorism.Subject = subject;

                return Ok(_mapper.Map<AphorismDto>(aphorism));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomAphorism(string subject)
        {
            var aphorism = await _aphorismsService.GetRandomAphorism();

            if (aphorism != null)
            {
                if (!string.IsNullOrEmpty(subject))
                    aphorism.Subject = subject;

                return Ok(_mapper.Map<AphorismDto>(aphorism));
            }
            else
            {
                return NotFound();
            }
        }
    }
}

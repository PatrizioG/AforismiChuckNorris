using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Aphorism> Aphorisms { get; set; }
        public int? MaxPendingRequest { get; set; }
    }
}

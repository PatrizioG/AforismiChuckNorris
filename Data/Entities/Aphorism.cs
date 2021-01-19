using ChuckNorrisAphorisms.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Data.Entities
{
    public class Aphorism
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public string Subject { get; set; }
        public string Culture { get; set; }
        public AphorismStatus Status { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

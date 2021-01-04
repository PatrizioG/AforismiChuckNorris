using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Data.Entities
{
    public class Aphorism
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Value { get; set; }
        public string Subject { get; set; }
        public string Culture { get; set; }
    }
}

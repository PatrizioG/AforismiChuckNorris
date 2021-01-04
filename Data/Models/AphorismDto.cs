using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Data.Models
{
    public class AphorismDto
    {
        public Guid Id { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string Value { get; set; }        
    }
}

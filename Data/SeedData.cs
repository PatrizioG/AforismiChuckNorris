using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Data
{
    public class SeedData
    {
        public static void SeedAphorisms(ApplicationDbContext context)
        {
            if (!context.Aphorisms.Any())
            {
                context.Aphorisms.Add(new Entities.Aphorism()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Value = "I bambini hanno paura del buio. Il buio ha paura di {0}",
                    Culture = "it-IT"
                });

                context.Aphorisms.Add(new Entities.Aphorism()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Value = "Quando l'uomo nero va a dormire, ogni notte controlla il suo armadio per vedere se c'è {0}",
                    Culture = "it-IT"
                });

                context.Aphorisms.Add(new Entities.Aphorism()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Value = "{0} può dividere per zero",
                    Culture = "it-IT"
                });

                context.SaveChanges();
            }
        }
    }
}

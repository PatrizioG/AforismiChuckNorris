using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Data
{
    public class SeedData
    {
        public static void SeedAphorisms(ILogger logger, ApplicationDbContext context, string path, string culture = "it-IT")
        {
            try
            {               

                // This text is added only once to the file.
                if (File.Exists(path))
                {
                    // Open the file to read from.
                    string[] readText = File.ReadAllLines(path);
                    foreach (string s in readText)
                    {
                        if (string.IsNullOrEmpty(s))
                            continue;

                        if (!s.Contains("{0}"))
                            continue;

                        context.Aphorisms.Add(new Entities.Aphorism()
                        {
                            Id = Guid.NewGuid(),
                            CreationDate = DateTime.UtcNow,
                            UpdateDate = DateTime.UtcNow,
                            Value = s.Trim(),
                            Culture = culture
                        });

                        logger.LogInformation($"Added: {s}");
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            if (context.Aphorisms.Any())
                return;
        }

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

using ChuckNorrisAphorisms.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Data
{
    public class SeedData
    {
        public static async Task SeedAphorismsAsync(
            ILogger logger, 
            ApplicationDbContext context, 
            IAphorismsService aphorismsService, 
            string path, string culture = "it-IT")
        {
            try
            {
                
                if (context.Aphorisms.Any())
                    return;
                
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

                        await aphorismsService.AddAphorism(s, culture, null, false);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }            
        }              
    }
}

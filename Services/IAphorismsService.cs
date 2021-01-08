using AforismiChuckNorris.Data.Entities;
using AforismiChuckNorris.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Services
{
    public interface IAphorismsService
    {
        Task<Aphorism> GetRandomAphorism();
        Task<Aphorism> GetAphorism(Guid aphorismId);
        IEnumerable<Aphorism> GetAphorismsOwnedBy(string userId);
        int GetAphorismCount();
        Task DeleteAphorism(Guid aphorismId);
        Task<bool> AddAphorism(string aphorism, string culture, string userId = null, bool saveToDb = true, AphorismStatus status = AphorismStatus.Published);
    }
}

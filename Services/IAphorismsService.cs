using ChuckNorrisAphorisms.Data.Entities;
using ChuckNorrisAphorisms.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Services
{
    public interface IAphorismsService
    {
        Task<Aphorism> GetRandomAphorism();        
        Task<IEnumerable<Aphorism>> GetPendingAphorism();
        Task<Aphorism> GetAphorism(Guid aphorismId);
        IEnumerable<Aphorism> GetAphorismsOwnedBy(string userId);
        int GetAphorismCount();
        Task DeleteAphorism(Guid aphorismId);
        Task<bool> AddAphorism(string aphorism, string culture, string userId = null, bool saveToDb = true, AphorismStatus status = AphorismStatus.Published);
        Task EditAphorism(Aphorism aphorism);
    }
}

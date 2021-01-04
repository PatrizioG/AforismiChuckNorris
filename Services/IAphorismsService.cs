using AforismiChuckNorris.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AforismiChuckNorris.Services
{
    public interface IAphorismsService
    {
        public Task<Aphorism> GetRandomAphorism();
        public Task<Aphorism> GetAphorism(Guid aphorismId);
        public Task DeleteAphorism(Guid aphorismId);

    }
}

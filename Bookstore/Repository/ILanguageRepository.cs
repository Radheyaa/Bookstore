using Bookstore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetAllLanguages();
    }
}
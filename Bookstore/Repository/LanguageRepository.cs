using Bookstore.Data;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private BookStoreContext _context;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetAllLanguages()
        {
            return await _context.Language.Select(x => new LanguageModel()
            {
                LanguageId = x.LanguageId,
                Name = x.Name
            }
            ).ToListAsync();
        }



    }
}

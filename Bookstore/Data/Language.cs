using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}

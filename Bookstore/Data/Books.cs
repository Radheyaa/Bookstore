using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Pages { get; set; }
        public string CoverPageURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<BookGallery>  bookGallary { get; set; }
    }
}

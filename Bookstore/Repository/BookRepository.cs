using Bookstore.Data;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class BookRepository
    {
        private BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Title = book.Title,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Pages = book.Pages
                    }); ;
                }
            }

            return books;

            //return DataSource();
        }

        public async Task<BookModel> GetBookId(int Id) 
        {

            var book = await _context.Books.FindAsync(Id);

            if ( book != null)
            {
                var bookmodel = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Title = book.Title,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Pages = book.Pages
                };
                return (bookmodel);
            }

            return null;
            //return DataSource().Where(X => X.Id == Id).FirstOrDefault();
        }

        public async Task<int> AddBook(BookModel book)
        {
            var newbook = new Books
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Pages = book.Pages,
                Language = book.Language,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _context.Books.AddAsync(newbook);
            await _context.SaveChangesAsync();

            return newbook.Id;
        }


        public List<BookModel> SearchBook(String Title, String Author)
        {
            return DataSource().Where(X => X.Author == Author && X.Title == Title).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){ Id = 1, Author="Prashant", Title = "ASP" , Description = "This is the description for ASP Book" , Category="Programming", Language="English", Pages="1054"},
                new BookModel(){ Id = 2, Author="Raj", Title = "Java" , Description = "This is the description for Java Book hello hello hello hello", Category="Framework", Language="English", Pages="2100"},
                new BookModel(){ Id = 3, Author="Mandar", Title = "C#" , Description = "This is the description for C# Book" , Category="Coding", Language="English", Pages="1654"},
                new BookModel(){ Id = 4, Author="John", Title = "Azure" , Description = "This is the description for Azure Book", Category="Programming", Language="English", Pages="521"}
            };
        }

    }
}

using Bookstore.Data;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class BookRepository : IBookRepository
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
                        LanguageId = book.LanguageId,
                        CoverImageURL = book.CoverPageURL,
                        Pages = book.Pages
                    }); ;
                }
            }

            return books;

            //return DataSource();
        }

        public async Task<BookModel> GetBookId(int id)
        {

            var book = await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Title = book.Title,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    CoverImageURL = book.CoverPageURL,
                    Pages = book.Pages,
                    Gallary = book.bookGallary.Select(g => new GallaryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList()
                }).FirstOrDefaultAsync();

            return book;

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
                LanguageId = book.LanguageId,
                CoverPageURL = book.CoverImageURL,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            newbook.bookGallary = new List<BookGallery>();

            foreach (var file in book.Gallary)
            {
                newbook.bookGallary.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newbook);
            await _context.SaveChangesAsync();

            return newbook.Id;
        }


        public List<BookModel> SearchBook(String Title, String Author)
        {
            // return DataSource().Where(X => X.Author == Author && X.Title == Title).ToList();

            return null;


        }

        //public List<LanguageModel> GetLanguages()
        //{
        //    return new List<LanguageModel>()
        //    {

        //        new LanguageModel{ Id = 1 , Text = "English" },
        //        new LanguageModel{ Id = 2 , Text = "French" },
        //        new LanguageModel{ Id = 3 , Text = "Spanish" }
        //    };
        //}

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){ Id = 1, Author="Prashant", Title = "ASP" , Description = "This is the description for ASP Book" , Category="Programming", Language="English", Pages=1054},
        //        new BookModel(){ Id = 2, Author="Raj", Title = "Java" , Description = "This is the description for Java Book hello hello hello hello", Category="Framework", Language="English", Pages=100},
        //        new BookModel(){ Id = 3, Author="Mandar", Title = "C#" , Description = "This is the description for C# Book" , Category="Coding", Language="English", Pages=154},
        //        new BookModel(){ Id = 4, Author="John", Title = "Azure" , Description = "This is the description for Azure Book", Category="Programming", Language="English", Pages=521}
        //    };
        //}

    }
}

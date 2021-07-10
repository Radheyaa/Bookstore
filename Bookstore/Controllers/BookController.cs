using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookrepository;
        private ILanguageRepository _languagerepository;
        private IWebHostEnvironment _env;

        public BookController(IBookRepository bookrepository , ILanguageRepository languagerepository , IWebHostEnvironment env)
        {
            _bookrepository = bookrepository;
            _languagerepository = languagerepository;
            _env = env;
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookrepository.GetAllBooks();
            return View(books);                
        }

        public IActionResult Index()
        {
            return View(_bookrepository.GetAllBooks());
        }

        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookrepository.GetBookId(id);
            //return View(_bookrepository.GetBookId(id));     

            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Addbook(bool isSuccess = false , int bookId = 0)
        {
            var model = new BookModel();
            
            ViewBag.language = new SelectList(await _languagerepository.GetAllLanguages() , "LanguageId", "Name" , 1);

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);            
        }

        [HttpPost]
        public async Task<IActionResult> Addbook(BookModel book )
        {
            if (ModelState.IsValid)
            {
                if(book.CoverPhoto != null )
                {
                    string folder = "books/cover/";
                    book.CoverImageURL = await UploadImage(folder, book.CoverPhoto);
                }

                if (book.GallaryFiles != null)
                {
                    string folder = "books/Gallary/";

                    book.Gallary = new List<GallaryModel>();   

                    foreach (var file in book.GallaryFiles)
                    {
                        var gallary = new GallaryModel() 
                        { 
                            Name = file.FileName,
                            URL = await UploadImage(folder,file)
                        };

                        book.Gallary.Add(gallary);
                    }                    
                }

                int id = await _bookrepository.AddBook(book);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Addbook), new { IsSuccess = true, BookId = id });
                }
            }

            ViewBag.language = new SelectList(await _languagerepository.GetAllLanguages(), "LanguageId", "Name", 1);
            return View();
        }

        private async Task<string> UploadImage(string folderPath , IFormFile file )
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverfolder = Path.Combine(_env.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverfolder, FileMode.Create));

            return "/" + folderPath.Replace(" ", "%20");
        }

        public List<BookModel> SearchBook(String Title, string Author)
        {
            return _bookrepository.SearchBook(Title, Author);     
                        
        }

    }
}

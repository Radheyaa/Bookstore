using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private BookRepository _bookrepository;
                 
        public BookController(BookRepository bookrepository )
        {
            _bookrepository = bookrepository;
            
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

        public IActionResult Addbook(bool isSuccess = false , int bookId = 0)
        {
            
                ViewBag.IsSuccess = isSuccess;
                ViewBag.BookId = bookId;
                return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Addbook(BookModel book )
        {
            if (ModelState.IsValid)
            {
                int id = await _bookrepository.AddBook(book);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Addbook), new { IsSuccess = true, BookId = id });
                }
            }
            return View();
        }

        public List<BookModel> SearchBook(String Title, string Author)
        {
            return _bookrepository.SearchBook(Title, Author);
        }

    }
}

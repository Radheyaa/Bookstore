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
        public BookController(BookRepository bookrepository)
        {
            _bookrepository = bookrepository;
        }
        public IActionResult GetAllBooks()
        {
            return View(_bookrepository.GetAllBooks());
        }

        public IActionResult Index()
        {
            return View(_bookrepository.GetAllBooks());
        }

        public IActionResult GetBook(int id)
        {
            return View(_bookrepository.GetBookId(id));                
        }

        public List<BookModel> SearchBook(String Title, string Author)
        {
            return _bookrepository.SearchBook(Title, Author);
        }

    }
}

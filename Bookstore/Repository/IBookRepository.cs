using Bookstore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddBook(BookModel book);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookId(int id);
        List<BookModel> SearchBook(string Title, string Author);
    }
}
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookId(int Id) 
        {
            return DataSource().Where(X => X.Id == Id).FirstOrDefault();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.API.Models
{
    public static class DataHelper
    {
        public static List<Book> PrepareBooks()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);

            List<Book> books = new List<Book>();

            for (int i = 0; i < 100; i++)
            {
                Book book = new Book();

                book.Name = string.Format("Book: {0}", i);
                book.Price = 50 + rnd.Next(25) - 25;
                book.IssueDate = DateTime.Now.AddDays(rnd.Next(100));

                books.Add(book);
            }

            return books;
        }
    }
}
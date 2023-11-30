using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Catalog
    {
        private readonly Lab3Context db;
        public Catalog(Lab3Context _db)
        {
            db = _db;
        }
        public void AddBook(Book book)
        {
                db.Books.Add(book);
                db.SaveChanges();
        }

        public List<Book> SearchByTitle(string searchByTitle)
        {
            return db.Books.Where(book => book.Title.Contains(searchByTitle)).ToList();
        }

        public List<Book> SearchByAuthor(string searchByAuthor)
        {
            return db.Books.Where(book => book.Author.Contains(searchByAuthor)).ToList();
        }

        public List<Book> SearchByISBN(string searchByISBN)
        {
            return db.Books.Where(book => book.ISBN == searchByISBN).ToList();
        }


        public List<Book> SearchByKeywords(List<string> keywords)
        {
            List<Book> result = new List<Book>();
            foreach (var book in db.Books)
            {
                if (keywords.Count(keyword => book.Title.Contains(keyword) || book.Annotation.Contains(keyword)) != 0)
                    result.Add(book);
            }
            return result;
        }
    }
}

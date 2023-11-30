using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lab3.Tests
{
    public class CatalogTests
    {
        [Fact]
        public void AddBook_ShouldAddBookToDatabase()
        {
            // Установка
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            //dbContext.Books.ToList().Returns(new List<Book>());

            // Действие
            var bookToAdd = new Book { Title = "Преступление и наказание", Author = "Ф.М. Достоевский", ISBN = "1234567890" };
            catalog.AddBook(bookToAdd);

            // Проверка
            dbContext.Books.Received().Add(bookToAdd);
            dbContext.Received().SaveChanges();
        }

        [Fact]
        public void SearchByTitle_ShouldReturnCorrectResults()
        {
            // Установка
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Title = "Преступление и наказание" };
            var book2 = new Book { Title = "Война и мир" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // Действие
            var results = catalog.SearchByTitle("Преступление и наказание");

            // Проверка
            Assert.Single(results);
            Assert.Contains(book1, results);

        }

        [Fact]
        public void SearchByAuthor_ShouldReturnCorrectResults()
        {
            // Установка
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Author = "Ф.М. Достоевский" };
            var book2 = new Book { Author = "Л.Н. Толстой" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // Действие
            var results = catalog.SearchByAuthor("Ф.М. Достоевский");

            // Проверка
            Assert.Single(results);
            Assert.Contains(book1, results);

        }

        [Fact]
        public void SearchByISBN_ShouldReturnCorrectResult()
        {
            // Arrange
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book = new Book { Title = "Test Book", ISBN = "1234567890" };

            dbContext.Books.Returns(new List<Book> { book }.AsQueryable());

            // Act
            var result = catalog.SearchByISBN("1234567890");

            // Assert
            Assert.Single(result);
            Assert.Contains(book, result);
        }


        [Fact]
        public void SearchByKeywords_ShouldReturnCorrectResults()
        {
            // Установка
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Title = "Преступление и наказание", Annotation = "Замечательная книга" };
            var book2 = new Book { Title = "Война и мир", Annotation = "Эпическое произведение" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // Действие
            var keywords = new List<string> { "замечательная", "эпическое" };
            var results = catalog.SearchByKeywords(keywords);

            // Проверка
            Assert.Equal(2, results.Count);
            Assert.Contains(book1, results);
            Assert.Contains(book2, results);

        }
    }
}

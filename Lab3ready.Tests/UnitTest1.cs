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
            // ���������
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            //dbContext.Books.ToList().Returns(new List<Book>());

            // ��������
            var bookToAdd = new Book { Title = "������������ � ���������", Author = "�.�. �����������", ISBN = "1234567890" };
            catalog.AddBook(bookToAdd);

            // ��������
            dbContext.Books.Received().Add(bookToAdd);
            dbContext.Received().SaveChanges();
        }

        [Fact]
        public void SearchByTitle_ShouldReturnCorrectResults()
        {
            // ���������
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Title = "������������ � ���������" };
            var book2 = new Book { Title = "����� � ���" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // ��������
            var results = catalog.SearchByTitle("������������ � ���������");

            // ��������
            Assert.Single(results);
            Assert.Contains(book1, results);

        }

        [Fact]
        public void SearchByAuthor_ShouldReturnCorrectResults()
        {
            // ���������
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Author = "�.�. �����������" };
            var book2 = new Book { Author = "�.�. �������" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // ��������
            var results = catalog.SearchByAuthor("�.�. �����������");

            // ��������
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
            // ���������
            var dbContext = Substitute.For<Lab3Context>();
            var catalog = new Catalog(dbContext);

            var book1 = new Book { Title = "������������ � ���������", Annotation = "������������� �����" };
            var book2 = new Book { Title = "����� � ���", Annotation = "��������� ������������" };
            var fakeData = new List<Book> { book1, book2 };

            dbContext.Books.Returns(fakeData.AsQueryable());

            // ��������
            var keywords = new List<string> { "�������������", "���������" };
            var results = catalog.SearchByKeywords(keywords);

            // ��������
            Assert.Equal(2, results.Count);
            Assert.Contains(book1, results);
            Assert.Contains(book2, results);

        }
    }
}

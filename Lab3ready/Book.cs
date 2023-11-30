using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genres { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Annotation { get; set; }
        [Key]
        public string ISBN { get; set; }
        public bool KeywordFoundInAnnotation { get; set; }

        public void PrintInfo()
        {
            Console.WriteLine($"Название книги: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Жанры: {Genres}");
            Console.WriteLine($"Дата публикации: {PublicationDate.ToShortDateString()}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine();
        }
    }
}

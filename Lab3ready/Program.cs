using Lab3;

var db = new Lab3Context();
db.Database.EnsureCreated();
//db.Books.Add(new Book { Title = "War and Peace", Author = "Lev Tolstoy", ISBN = "19281301313" });
//db.Books.Add(new Book { Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "34091827465" });
db.SaveChanges();
//foreach (var book in db.Books)
//    Console.WriteLine(book.Title);

Catalog catalog = new Catalog(db);

        while (true)
        {
            Console.WriteLine("1) Добавление книги в каталог");
            Console.WriteLine("2) Выборка информации о книге по названию или его фрагменту");
            Console.WriteLine("3) Выборка информации о книге по имени автора или ISBN");
            Console.WriteLine("4) Выборка информации о книге по ключевым словам");
            Console.WriteLine("5) Выход");

            Console.Write("Выберите операцию (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название книги: ");
                    string title = Console.ReadLine();
                    Console.Write("Введите имя автора: ");
                    string author = Console.ReadLine();
                    Console.Write("Введите жанры (через запятую): ");
                    string genres = Console.ReadLine();
                    Console.Write("Введите дату публикации (гггг-мм-дд): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime publicationDate))
                    {
                        Console.Write("Введите ISBN: ");
                        string isbn = Console.ReadLine();
                        Console.Write("Введите аннотацию: ");
                        string annotation = Console.ReadLine();

                        catalog.AddBook(new Book
                        {
                            Title = title,
                            Author = author,
                            Genres = genres,
                            PublicationDate = publicationDate,
                            ISBN = isbn,
                            Annotation = annotation
                        });

                        Console.WriteLine("Книга успешно добавлена в каталог.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат даты. Книга не была добавлена.");
                    }

                    break;


                case "2":
                    Console.Write("Введите название или его фрагмент: ");
                    string titlePut = Console.ReadLine();
                    var titleResults = catalog.SearchByTitle(titlePut);

                    Console.WriteLine("Результаты поиска:");
                    foreach (var book in titleResults)
                    {
                        book.PrintInfo();
                    }
                    break;

                case "3":
                    Console.Write("Введите имя автора или ISBN: ");
                    string authorOrISBN_put = Console.ReadLine();
                    var authorOrISBNResults = catalog.SearchByAuthor(authorOrISBN_put);
                    authorOrISBNResults.AddRange(catalog.SearchByISBN(authorOrISBN_put));

                    Console.WriteLine("Результаты поиска:");
                    foreach (var book in authorOrISBNResults)
                    {
                        book.PrintInfo();
                    }
                    break;

                case "4":
                    Console.Write("Введите ключевые слова (через запятую): ");
                    List<string> keyword_put = Console.ReadLine().Split(',').Select(k => k.Trim()).ToList();
                    var keywordResults = catalog.SearchByKeywords(keyword_put);

                    Console.WriteLine("Результаты поиска:");
                    foreach (var book in keywordResults)
                    {
                        book.PrintInfo();
                        book.KeywordFoundInAnnotation = keyword_put.Any(keyword => book.Annotation.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                        Console.WriteLine($"Ключевое слово найдено {(book.KeywordFoundInAnnotation ? "в аннотации" : "не в аннотации")}");
                        Console.WriteLine();
                    }
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите операцию от 1 до 5.");
                    break;
            }
        }

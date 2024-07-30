using ConsoleTables;
using BookManagementApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementApp
{
    public class BookService
    {
        private readonly BookDbContext _context;

        public BookService()
        {
            _context = new BookDbContext();
        }


        private bool IsValidString(string input)
        {
            return input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }


        //Get All Books
        public void GetAllBooks()
        {
            //return _context.Books.ToList();
            Console.WriteLine("\nShowing All Books\n");

            var books = _context.Books.ToList();

            var table = new ConsoleTable("ID", "Title", "Author", "Price", "AvailableCopies", "Genre");

            foreach (var book in books)
            {
                table.AddRow(book.BookID, book.Title, book.Author, book.Price, book.AvailableCopies, book.Genre);

                //Console.WriteLine($"{book.BookID}  |  {book.Title} {book.Author} {book.Price} {book.AvailableCopies} {book.Genre}");
            }
            table.Write();
        }

        //public List<Books> GetAllBooks1()
        //{
        //   return _context.Books.ToList();
        // }


        //Inserting Book Records
        public void AddBook()
        {

            Console.Write("\nEnter Book Title: ");

        jump1:
            var title = Console.ReadLine();
            if ((!IsValidString(title)))

            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump1;

            }
            else if ((string.IsNullOrEmpty(title)))
            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump1;
            }

            Console.Write("Enter Authorname: ");
        jump2:
            var author = Console.ReadLine();
            if ((!IsValidString(author)))

            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump2;

            }
            else if ((string.IsNullOrEmpty(author)))
            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump2;
            }
            decimal price;
            Console.Write("Enter Price: ");
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive decimal number for the price:");
            }
            //decimal price = Convert.ToDecimal(Console.ReadLine());
            int availableCopies;
            Console.Write("Enter availableCopies: ");
            while (!int.TryParse(Console.ReadLine(), out availableCopies) || availableCopies < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number for the copies:");
            }
            // int availableCopies = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Book Genre: ");
        jump3:
            var genre = Console.ReadLine();

            if ((!IsValidString(genre)))

            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump3;

            }
            else if ((string.IsNullOrEmpty(genre)))
            {
                Console.WriteLine("Invalid input. Only characters are allowed and fields cannot be empty.Enter Valid Title");
                goto jump3;
            }


            var book = new Books
            {
                Title = title,
                Author = author,
                Price = price,
                AvailableCopies = availableCopies,
                Genre = genre
            };
            // bookService.AddBook(book);

            Console.WriteLine("\nBook added successfully!");

            _context.Books.Add(book);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }

        //Updated Books
        public void UpdateBook()
        {
            var books = _context.Books.ToList();

            var table = new ConsoleTable("ID", "Title", "Author", "Price", "AvailableCopies", "Genre");

            foreach (var book1 in books)
            {
                table.AddRow(book1.BookID, book1.Title, book1.Author, book1.Price, book1.AvailableCopies, book1.Genre);

                //Console.WriteLine($"{book.BookID}  |  {book.Title} {book.Author} {book.Price} {book.AvailableCopies} {book.Genre}");
            }
            table.Write();

            Console.Write("\nEnter the ID from the above table which book you want to update: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please Enter a valid BookId to Continue");
            }
            // var book = _context.Books.Find(BookID);
            var book = _context.Books.FirstOrDefault(b => b.BookID == id);
            if (book == null)
            {
                Console.WriteLine("\nYour Book is not found. Please look forward below.");
                return;
            }

            Console.Write("\nEnter new Title (leave blank to keep current):");
        jump2:
            string title = Console.ReadLine();
            if ((!IsValidString(title)))
            {
                Console.WriteLine("\nInvalid input. Only characters are allowed. Input Valid Title");
                goto jump2;
            }

            if (!string.IsNullOrEmpty(title))
            {
                book.Title = title;
            }


            Console.Write("Enter new Author (leave blank to keep current): ");
        jump3:
            string author = Console.ReadLine();
            if ((!IsValidString(author)))
            {
                Console.WriteLine("\nInvalid input. Only characters are allowed. Input Valid Title");
                goto jump3;
            }

            if (!string.IsNullOrEmpty(author))
            {
                book.Author = author;
            }


            Console.Write("Enter new Price (leave blank to keep current):");
        jump4:
            string InputPrice = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(InputPrice))
            {
                if (decimal.TryParse(InputPrice, out decimal newPrice) && newPrice >= 0)
                {
                    book.Price = newPrice;
                }
                else
                {
                    Console.WriteLine("\nInvalid price input. Please enter a valid positive decimal number for the price:");
                    goto jump4;
                }
            }


            //  bookService.UpdateBook(book);

            Console.Write("Enter new availableCopies (leave blank to keep current):");
        jump5:
            string userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                if (int.TryParse(userInput, out int newcopies) && newcopies >= 0)
                {
                    book.AvailableCopies = newcopies;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid input number for the copies:");
                    goto jump5;
                }
            }

        //   bookService.UpdateBook(book);
        // book.AvailableCopies = Convert.ToInt32(Console.ReadLine());

        jump6:
            Console.Write("Enter New Book Genre (leave blank to keep current): ");
            string genre = Console.ReadLine();
            if ((!IsValidString(genre)))
            {
                Console.WriteLine("Invalid input. Only characters are allowed.Enter Valid Genre");
                goto jump6;
            }

            if (!string.IsNullOrEmpty(genre))
            {
                book.Genre = genre;
            }

            // bookService.UpdateBook(book);
            Console.WriteLine("\nYour Book is Updated Successfully");

            //  _context.Books.Update(book);
            _context.SaveChanges();
        }
        public static int BookID;

        public Books GetBookById(int id) => _context.Books.Find(id);

        public void DeleteBook()
        {

            var books = _context.Books.ToList();
            var table1 = new ConsoleTable("ID", "Title", "Author", "Price", "AvailableCopies", "Genre");
            foreach (var book1 in books)
            {
                table1.AddRow(book1.BookID, book1.Title, book1.Author, book1.Price, book1.AvailableCopies, book1.Genre);
            }
            table1.Write();

            Console.WriteLine("\nEnter the Book ID which you want to delete from the above table: ");
            int BookID;
            while (!int.TryParse(Console.ReadLine(), out BookID))
            {
                Console.WriteLine("Please Enter a Valid BookId to Continue");
            }
            var book = _context.Books.Find(BookID);
            if (book != null)
            {

                _context.Books.Remove(book);
                _context.SaveChanges();
                Console.WriteLine("\nBook Deleted Successfully");

            }
            else
            {
                Console.WriteLine("Your Book is not found Please look forward below.");
            }


        }
        public void RetrieveBooksByAuthor()
        {
            Console.Write("Enter author name: ");
            string author = Console.ReadLine();

            // Query books by the specified author

            var books = _context.Books.Where(b => b.Author == author).ToList();

            // Display the books by the author
            Console.WriteLine($"Books by {author}:");

            var table = new ConsoleTable("ID", "Title", "Author", "Price", "AvailableCopies", "Genre");

            foreach (var book in books)
            {
                table.AddRow(book.BookID, book.Title, book.Author, book.Price, book.AvailableCopies, book.Genre);

                //Console.WriteLine($"{book.BookID}  |  {book.Title} {book.Author} {book.Price} {book.AvailableCopies} {book.Genre}");
            }
            table.Write();
        }

    }
}

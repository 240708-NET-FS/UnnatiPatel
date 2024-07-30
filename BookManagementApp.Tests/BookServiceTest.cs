using BookManagementApp;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.InMemory;
using BookManagementApp.Entities;


namespace BookManagementApp.Tests;

public class BookServiceTest
{
    private DbContextOptions<BookDbContext> InMemoryOptions()
    {
        return new DbContextOptionsBuilder<BookDbContext>()
            .UseInMemoryDatabase(databaseName: "BookStore")
            .Options;
    }
    [Fact]
    public void AddBook_ShouldAddBookDb()
    {
        var options = InMemoryOptions();

        // Insert a new entity
        using (var _context = new BookDbContext(options))
        {
            var book = new Books { Title = "The Book", Author = "The Author", Price = 9.99M, AvailableCopies = 5, Genre = "The Genre" };
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        // Verify the entity was inserted
        using (var _context = new BookDbContext(options))
        {
            var addedBook = _context.Books.FirstOrDefault(b => b.Title == "The Book");
            Assert.NotNull(addedBook);
            Assert.Equal(2, _context.Books.Count());

        }


    }

    [Fact]
    public void UpdateBook_ShouldUpdateBookDb()
    {
        var options = InMemoryOptions();

        // Insert a new entity
        using (var _context = new BookDbContext(options))
        {
            _context.Books.Add(new Books { BookID = 1, Title = "Original Book", Author = "Original Author", Price = 10.0m, AvailableCopies = 5, Genre = "Original Genre" });

            _context.SaveChanges();
        }
        //Act
        using (var _context = new BookDbContext(options))
        {

            var book = _context.Books.Find(1);
            book.Title = "Updated Title";
            book.Author = "Updated Author";
            book.Price = 5.55m;
            book.AvailableCopies = 7;
            book.Genre = "Updated Genre";
            _context.SaveChanges();


        }
        using (var _context = new BookDbContext(options))
        {
            var book = _context.Books.Find(1);
            Assert.Equal("Updated Title", book.Title);
            Assert.Equal("Updated Author", book.Author);
            Assert.Equal(5.55m, book.Price);
            Assert.Equal(7, book.AvailableCopies);
            Assert.Equal("Updated Genre", book.Genre);

        }


    }

    [Fact]
    public void DeleteBook_Testdb()
    {
        var options = InMemoryOptions();

        using (var _context = new BookDbContext(options))
        {
            _context.Books.Add(new Books { Title = "Book to Delete", Author = "Author", Price = 10.99m });
            _context.SaveChanges();
        }

        using (var _context = new BookDbContext(options))
        {
            var book = _context.Books.Find(1);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        using (var _context = new BookDbContext(options))
        {
            var book = _context.Books.Find(1);
            Assert.Null(book);
        }
    }


}



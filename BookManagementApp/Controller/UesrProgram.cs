using ConsoleTables;
using BookManagementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Microsoft.Identity.Client;


namespace BookManagementApp.Entities
{
    class Program
    {
        static void Main(string[] args)
        {

            var bookService = new BookService();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n========================\nBook Management System \n========================");

            jump1:
                Console.WriteLine("\nOperations:");
                Console.WriteLine("[1] Add New Book");
                Console.WriteLine("[2] View All Books");
                Console.WriteLine("[3] Update Books");
                Console.WriteLine("[4] Delete Books");
                Console.WriteLine("[5] View Books By Author");
                Console.WriteLine("[6] Exit");
                Console.Write("\nEnter Your Operation: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Call the method to insert a new book
                        bookService.AddBook();
                        goto jump1;

                    case "2":
                        // Call the method to retrieve and display all books
                        bookService.GetAllBooks();
                        goto jump1;
                    case "3":
                        // Call the method to update books
                        bookService.UpdateBook();
                        goto jump1;
                    case "4":
                        // Call the method to delete a book
                        bookService.DeleteBook();
                        goto jump1;
                    case "5":
                        // Call the method to delete a book
                        bookService.RetrieveBooksByAuthor();
                        goto jump1;
                    case "6":
                        // Set the exit flag to true to exit the application
                        exit = true;
                        break;
                    default:
                        // Handle invalid menu options
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }


            }
            // Display message when exiting the application
            Console.WriteLine("\nThank You For Using the App! \n");

        }
    }
}
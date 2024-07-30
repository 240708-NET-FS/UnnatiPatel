using System.ComponentModel.DataAnnotations;

namespace BookManagementApp.Entities;

public class Books

{

    [Key]

    public int BookID { get; set; }


    public string Title { get; set; }

    public string Author { get; set; }

    public decimal Price { get; set; }

    public int AvailableCopies { get; set; }

    public string Genre { get; set; }



}
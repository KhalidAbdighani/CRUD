using System;

namespace BookLibrary.Domain.Entities;

public class BookColums
{   
    public int id {get;set;}
    public required string Book_name_db {get;set;}
    public required string Auth_name_db {get;set;}
    public int CategoryId {get; set;}
    public Category? Category {get;set;}

}

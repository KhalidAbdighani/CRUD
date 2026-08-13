using System;

namespace BookLibrary.Domain.Entities;
using BookLibrary.Domain.Entities;

public class Category
{
    public int id {get;set;}
    public required string name {get; set;}
    public ICollection<BookColums> books  {get ;set;}= new List<BookColums>();
}

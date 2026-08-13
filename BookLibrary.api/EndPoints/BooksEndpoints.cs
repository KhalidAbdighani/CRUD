using System;
using BookLibrary.Application.DTOs;
using BookLibrary.Domain.Entities;
using BookLibrary.Infrastructure.Data;



using Microsoft.EntityFrameworkCore;

namespace BookLibrary.api.EndPoints;
public static class BooksEndpoints
{
        const string GetBookEndpointName = "GetBook";
        private static readonly List<LibraryDTO> books= [
        
    ];


        public static  void MapEndpoints (this IEndpointRouteBuilder app)
        {
                //GET BOOKS 
                app.MapGet("/books",() =>{
                if(books is []) return Results.NotFound("Empty");
                return Results.Ok(books); 
                }
                );


                //GET BOOK WITH ID
                // app.MapGet("/books/{id}", (int id) =>
                // {
                // var book = books.Find(book=>book.id==id);
                // return book is null? Results.NotFound("Book not found") : Results.Ok(book);
                // } )
                // .WithName(GetBookEndpointName);


                //POST BOOK
                app.MapPost("/books", async (PostBook book , AppDbContext db) =>
                {
                    if(string.IsNullOrEmpty(book.Book_name) ||
                     string.IsNullOrEmpty(book.Auth_name)||
                     book.CategoryId <0)
                     
                     return Results.BadRequest("Book, Auth names and category number are required!");

                    var categoryExists = await db.Categories.AnyAsync(c => c.id == book.CategoryId);
                    if (!categoryExists)
                    {
                    return Results.BadRequest($"category with id {book.CategoryId} does not Exists");
    }
                    
                    
                    var newbook = new BookColums
                    {   

                        Book_name_db= book.Book_name,
                        Auth_name_db=book.Auth_name,
                        CategoryId=book.CategoryId
                    };
                    
                    db.BookStore.Add(newbook);
                    await db.SaveChangesAsync();
                    return Results.Ok("done");
                });

                app.MapPost("/category", async( AppDbContext db) =>
                {
                    var categories = await db.Categories.Select( c => new{c.id,c.name}).ToListAsync();
                    await db.SaveChangesAsync();
                });

                 
                //UPDATE BOOK
                app.MapPut("/books/{id}", async (int id, PutBook UpdateBook, AppDbContext db) =>
                {
                var i = books.FindIndex(book=>book.id==id);
                var result_from_db = await db.BookStore.FirstOrDefaultAsync(target=>target.id==id);
                
                

                
                if ( result_from_db == null)return  Results.NotFound($"Book with id {id} wasnt found");
                
                

                
                if(UpdateBook.Auth_name ==result_from_db.Auth_name_db && UpdateBook.Book_name == result_from_db.Book_name_db  )return Results.BadRequest($"No changes were made to the book with ID {id}");
                
                
                
                result_from_db?.Auth_name_db=UpdateBook.Auth_name;
                result_from_db?.Book_name_db=UpdateBook.Book_name;
                
                
                
                await db.SaveChangesAsync();
                


                
                return Results.Ok(new
                {
                    msg = $"Book with ID {id} was successfully Updated",
                    data=result_from_db
                });
                });


                //DELETE BOOK
                app.MapDelete("/books/{id}",async (int id, AppDbContext db)=> {
                var result_from_db = await db.BookStore.FindAsync(id);

                var i = books.Find(book=>book.id==id);
                if( result_from_db is null)return Results.NotFound($"No book found with ID {id}");
                
                
                db.BookStore.Remove(result_from_db);
                await db.SaveChangesAsync();
                return Results.Text($"Book with ID {id} was successfully deleted");
                });



        }

}

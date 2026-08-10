using System;

namespace Lib_app;

public static class BooksEndpoints
{
        const string GetBookEndpointName = "GetBook";
        private static readonly List<LibraryDTO> books= [
        new (1,"Book1","Author1"),
        new (2,"Book2","Author2"),
        new (3,"Book3","Author3")
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
                app.MapGet("/books/{id}", (int id) =>
                {
                var book = books.Find(book=>book.id==id);
                return book is null? Results.NotFound("Book not found") : Results.Ok(book);
                } )
                .WithName(GetBookEndpointName);


                //POST BOOK
                app.MapPost("/books", (PostBook book) =>
                {
                    if(string.IsNullOrEmpty(book.Book_name) || string.IsNullOrEmpty(book.Auth_name))return Results.BadRequest("Book and Auth names are required!");

                    LibraryDTO new_book= new(books.Count+1, book.Book_name, book.Auth_name);
                    books.Add(new_book);
                    return TypedResults.CreatedAtRoute(new_book,  GetBookEndpointName, new {id = new_book.id});
                });


                //UPDATE BOOK
                app.MapPut("/books/{id}", (int id, PutBook UpdateBook) =>
                {
                var i = books.FindIndex(book=>book.id==id);

                if (i < 0)return  Results.NotFound("Book wasnt found");
                var Bname = books[i].Book_name;
                var Aname =books[i].Auth_name;
                
                if(UpdateBook.Auth_name == Aname && UpdateBook.Book_name == Bname )return Results.BadRequest("No changes have been made");
                if(string.IsNullOrEmpty(UpdateBook.Auth_name)|| string.IsNullOrEmpty(UpdateBook.Book_name))return Results.BadRequest("new Book and Authors names are required");
                books[i]= new LibraryDTO (
                id,
                UpdateBook.Book_name,
                UpdateBook.Auth_name
                );
                
                return Results.Ok(books[i]);
                });


                //DELETE BOOK
                app.MapDelete("/books/{id}",(int id)=> {

                var i = books.Find(book=>book.id==id);
                if(i is null)return Results.NotFound("Book was not found");
                books.Remove(i);
                return Results.Text("Deleted successfully");
                });



        }

}

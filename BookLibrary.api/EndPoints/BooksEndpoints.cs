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
        app.MapGet("/books",() =>{
    if(books is []) return Results.NotFound("Empty");
    return Results.Ok(books); 
    }
    );

    app.MapGet("/books/{id}", (int id) =>
    {
    var book = books.Find(book=>book.id==id);
    return book is null? Results.NotFound("Book not found") : Results.Ok(book);
    } )
        .WithName(GetBookEndpointName);



    app.MapPost("/books", (PostBook book) =>
    {
    
    LibraryDTO new_book= new(books.Count+1, book.Book_name, book.Auth_name);
    books.Add(new_book);
    return TypedResults.CreatedAtRoute(new_book,  GetBookEndpointName, new {id = new_book.id});
    });



    app.MapPut("/books/{id}", (int id, PutBook UpdateBook) =>
{
    var i = books.FindIndex(book=>book.id==id);
    if (i < 0)return  Results.NotFound("Book wasnt found");
    
        books[i]= new LibraryDTO (
        id,
        UpdateBook.Book_name,
        UpdateBook.Auth_name
        
    );
     return Results.Ok(books[i]);

});


app.MapDelete("/books/{id}",(int id)=> {

    var i = books.Find(book=>book.id==id);
    if(i is null)return Results.NotFound("Book was not found");
    books.Remove(i);
    return Results.Text("Deleted successfully");
});
    

    




















    }

}

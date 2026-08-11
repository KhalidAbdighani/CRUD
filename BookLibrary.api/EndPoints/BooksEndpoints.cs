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
                app.MapPost("/books", async (PostBook book , AppDbContext db) =>
                {
                    if(string.IsNullOrEmpty(book.Book_name) || string.IsNullOrEmpty(book.Auth_name))return Results.BadRequest("Book and Auth names are required!");

                    LibraryDTO new_book= new(books.Count+1, book.Book_name, book.Auth_name);
                    books.Add(new_book);

                    var newbook = new BookColums
                    {
                        Book_name_db= book.Book_name,
                        Auth_name_db=book.Auth_name
                    };
                    db.BookStore.Add(newbook);
                    await db.SaveChangesAsync();
                    return TypedResults.CreatedAtRoute(new_book,  GetBookEndpointName, new {id = new_book.id});
                });

                 
                //UPDATE BOOK
                app.MapPut("/books/{id}", async (int id, PutBook UpdateBook, AppDbContext db) =>
                {
                var i = books.FindIndex(book=>book.id==id);
                var result_from_db = db.BookStore.FirstOrDefault(target=>target.id==id);
                
                

                if ( i< 0 && result_from_db == null)return  Results.NotFound("Book wasnt found");
                var Bname = books[i].Book_name;
                var Aname =books[i].Auth_name;

                
                if(UpdateBook.Auth_name == Aname && UpdateBook.Book_name == Bname && UpdateBook.Auth_name ==result_from_db.Auth_name_db && UpdateBook.Book_name == result_from_db.Book_name_db  )return Results.BadRequest($"No changes were made to the book with ID {id}");
                // if(string.IsNullOrEmpty(UpdateBook.Auth_name)|| string.IsNullOrEmpty(UpdateBook.Book_name))return Results.BadRequest("new Book and Authors names are required");
                
                books[i]= new LibraryDTO (
                id,
                UpdateBook.Book_name,
                UpdateBook.Auth_name
                );
                
                result_from_db?.Auth_name_db=UpdateBook.Auth_name;
                result_from_db?.Book_name_db=UpdateBook.Book_name;
                
                
                
                await db.SaveChangesAsync();
                


                
                return Results.Ok(new
                {
                    msg = $"Book with ID {id} was successfully Updated",
                    data=books[i]
                });
                });


                //DELETE BOOK
                app.MapDelete("/books/{id}",async (int id, AppDbContext db)=> {
                var result_from_db = await db.BookStore.FindAsync(id);

                var i = books.Find(book=>book.id==id);
                if(i is null && result_from_db is null)return Results.NotFound($"No book found with ID {id}");
                books.Remove(i);
                db.BookStore.Remove(result_from_db);
                await db.SaveChangesAsync();
                return Results.Text($"Book with ID {id} was successfully deleted");
                });



        }

}

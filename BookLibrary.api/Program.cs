using Lib_app;
const string GetBookEndpointName = "GetBook";
var builder = WebApplication.CreateBuilder(args);
//imports >> dependences


var app = builder.Build();
//routs and uses 

List<LibraryDTO> books= [
    new (1,"Book1","Author1"),
    new (2,"Book2","Author2"),
    new (3,"Book3","Author3")
];
//viwe books 
app.MapGet("/books",

 () =>{
    return books;
    }
);



app.MapGet("/books/{id}", (int id) => books.Find(book=>book.id==id))
        .WithName(GetBookEndpointName);



//add book
app.MapPost("/books", (PostBook book) =>
{
    
    LibraryDTO new_book= new(books.Count+1, book.Book_name, book.Auth_name);
    books.Add(new_book);
    return TypedResults.CreatedAtRoute(new_book,  GetBookEndpointName, new {id = new_book.id});
});

//Update book 
app.MapPut("/books/{id}", (int id, PutBook UpdateBook) =>
{
    var i = books.FindIndex(book=>book.id==id);
    books[i]= new LibraryDTO (
        id,
        UpdateBook.Book_name,
        UpdateBook.Auth_name
        
    );
    return Results.Text("Updated successfully");
});

//delete book 
app.MapDelete("/books/{id}",(int id)=> {
    var i = books.Find(book=>book.id==id);
    books.Remove(i);
    return Results.Text("Deleted successfully");
});




app.MapGet("/", () => "Hello World!");

app.Run();

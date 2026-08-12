using System.ComponentModel.DataAnnotations;

// namespace BookLibrary.Application.Dtos;

public record  DeleteBook(
    [Required] [StringLength(20)] string Book_name,
    [Required] [StringLength(20)] string Auth_name
);

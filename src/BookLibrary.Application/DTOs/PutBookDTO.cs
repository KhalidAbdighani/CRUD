using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Application.DTOs;
public record  PutBook(
   [Required] [StringLength(20)] string Book_name,
   [Required] [StringLength(20)] string Auth_name
);

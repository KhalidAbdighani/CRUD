using System.ComponentModel.DataAnnotations;
using BookLibrary.Domain.Entities;
namespace BookLibrary.Application.DTOs;

public record  PostBook(
    [Required] [StringLength(20)] string Book_name,
    [Required] [StringLength(20)] string Auth_name,
    [Required]                    int CategoryId
);

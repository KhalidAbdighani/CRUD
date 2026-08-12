using System.ComponentModel.DataAnnotations;

public record  PostBook(
    [Required] [StringLength(20)] string Book_name,
    [Required] [StringLength(20)] string Auth_name
);

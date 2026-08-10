using System.ComponentModel.DataAnnotations;

namespace Lib_app;

public record  DeleteBook(
    [Required] [StringLength(20)] string Book_name,
    [Required] [StringLength(20)] string Auth_name
);

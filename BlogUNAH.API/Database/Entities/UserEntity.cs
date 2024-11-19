using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;

public class UserEntity : IdentityUser
{
    [StringLength(70, MinimumLength = 3)]
    [Column("first_name")]
    [Required]
    public string FirstName { get; set; }

    [StringLength(70, MinimumLength = 3)]
    [Column("last_name")]
    [Required]
    public string LastName { get; set; }

    [StringLength(450)]
    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("refresh_token_expire")]
    public DateTime RefreshTokenExpire { get; set; }

    //manera de implementar que un usuario no pueda loguearse
    //public bool IsInactive { get; set; }
}

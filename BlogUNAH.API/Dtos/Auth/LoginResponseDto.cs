namespace BlogUNAH.API.Dtos.Auth;

//aqui se puede devolver cualquier cosa que necesite la aplicación
public class LoginResponseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; }
}

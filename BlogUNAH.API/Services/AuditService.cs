using BlogUNAH.API.Services.Interfaces;

namespace BlogUNAH.API.Services;

public class AuditService : IAuditService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(
        //httpcontextaccesor accede a las cabeceras de la petición
        IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        //esto es un claim
        var idClaim = _httpContextAccessor
            .HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();

        //este es el id (string)
        return idClaim.Value;

        //return "1dcd6bfc-67b1-4a56-a743-841863cb0a1f";
    }
}

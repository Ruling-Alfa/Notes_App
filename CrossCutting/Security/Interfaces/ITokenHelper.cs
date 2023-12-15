using CrossCutting.Security.Models;

namespace CrossCutting.Security.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateToken(TokenModel user);
    }
}
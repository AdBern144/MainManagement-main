using StockManagement.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StockManagement.Interfaces.Services;

public interface IUserService
{
    User? Login(string email, string password);

    Task<User?> RegisterAsync(string email, string password);

    User GetCurrentUser();
    JwtSecurityToken GetToken(User user);
}
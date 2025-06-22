using StockManagement.Models;

namespace StockManagement.Interfaces.Services;

public interface IUserService
{
    User? Login(string email, string password);
}

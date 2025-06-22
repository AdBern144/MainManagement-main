using StockManagement.Interfaces.Repositories;
using StockManagement.Interfaces.Services;
using StockManagement.Models;

namespace StockManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User? Login(string email, string password)
    {
        var user = _repository.GetByEmail(email);

        if (user != null)
        {
            if(user.Password == password)
            {
                user.LastLoginDate = DateTime.UtcNow;
                _repository.UpdateUser(user);

                return user;
            }
        }
        return null;
    }
}

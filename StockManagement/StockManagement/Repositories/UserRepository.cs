using StockManagement.Interfaces.Repositories;
using StockManagement.Models;

namespace StockManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public User? Get(string email)
    {
        try
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }
        catch
        {
            throw new Exception("Couldn't retrieve user by email");
        }

    }

    public User? Get(int id)
    {
        return _context.Users.SingleOrDefault(x => x.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var userToBeDeleted = Get(id);
        if (userToBeDeleted != null)
        {
            _context.Users.Remove(userToBeDeleted);
            _context.SaveChanges();
        }
    }
}
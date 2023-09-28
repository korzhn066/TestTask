using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db) 
        { 
            _db = db;
        }
        public async Task<User> GetUser()
        {
            var users = await _db.Users.Include(u => u.Orders).ToListAsync();

            var maxCount = 0;
            User result = new();

            foreach (var user in users)
            {
                if (user.Orders.Count > maxCount)
                {
                    maxCount = user.Orders.Count;
                    result.Status = user.Status;
                    result.Email = user.Email;
                    result.Id = user.Id;
                }
            }


            return result;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.Where(u => u.Status == UserStatus.Inactive).ToListAsync();
        }
    }
}

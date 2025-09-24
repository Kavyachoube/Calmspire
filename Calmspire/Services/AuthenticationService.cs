using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Services
{
    public class AuthenticationService
    {
        private readonly CalmSpireDbContext _context;
        public AuthenticationService(CalmSpireDbContext context) => _context = context;

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;

            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (exist != null) throw new InvalidOperationException("A user with this email already exists.");

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);
    }
}

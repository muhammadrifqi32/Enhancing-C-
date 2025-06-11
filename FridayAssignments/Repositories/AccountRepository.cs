using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;

        public AccountRepository(MyContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Account>();
        }

        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _context.Accounts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public bool VerifyPassword(Account account, string inputPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(account, account.Password, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}

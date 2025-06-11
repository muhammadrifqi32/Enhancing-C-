using FridayAssignments.Models;

namespace FridayAssignments.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmailAsync(string email);
        bool VerifyPassword(Account account, string inputPassword);
    }
}

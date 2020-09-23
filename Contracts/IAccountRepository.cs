using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> AccountsByOwner(Guid ownerId);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountById(Guid accountId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);

    }
}

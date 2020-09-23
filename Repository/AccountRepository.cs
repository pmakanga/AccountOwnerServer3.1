using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await FindAll()
                .OrderBy(ac => ac.AccountType)
                .ToListAsync();
        }

        public async Task<Account> GetAccountById(Guid accountId)
        {
            return await FindByCondition(account => account.Id.Equals(accountId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> AccountsByOwner(Guid ownerId)
        {
            return await FindByCondition(a => a.OwnerId.Equals(ownerId))
                .ToListAsync();
        }

        public void CreateAccount(Account account)
        {
             Create(account);
        }
        public void UpdateAccount(Account account)
        {
            Update(account);
        }
        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

        //public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        //{
        //    return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        //}
    }
}

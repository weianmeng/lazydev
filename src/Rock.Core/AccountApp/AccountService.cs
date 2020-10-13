﻿using LazyDev.Core.Utility;
using LazyDev.EFCore;
using Rock.Core.Entities;
using System.Threading.Tasks;
using LazyDev.Core.Exception;

namespace Rock.Core.AccountApp
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddAccountOutput> AddAsync(AccountInput accountInput)
        {

            var salt = RandomUtility.RandDomStr();
            var password = EncryptUtility.Md5(accountInput.Password + salt);

            if (string.IsNullOrEmpty(accountInput.NickName))
            {
                accountInput.NickName = RandomUtility.RandDomStr();
            }
            var account = new Account
            {
                NickName = accountInput.NickName,
                Salt = salt,
                Password = password,
                Email = accountInput.Email,
                Mobile = accountInput.Mobile
            };

            var resp = _unitOfWork.Repository<Account>();
            await resp.AddAsync(account);
            await _unitOfWork.CommitAsync();
            return new AddAccountOutput { Id = account.Id };
        }

        public async Task<AccountInfoOutput> GetAccountInfo(AccountInfoInput accountInfoInput)
        {
            var resp = _unitOfWork.Repository<Account>();
            var account = await resp.FindAsync(x => x.Mobile == accountInfoInput.Mobile);
            if (account == null)
            {
                throw new FriendlyException("未找到用户!");
            }
            return new AccountInfoOutput
            {
                NickName = account.NickName,
                Mobile = account.Mobile,
                Email = account.Email
            };
        }
    }
}

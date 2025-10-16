using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aDealerEDVMS.Repository.ToanHH;
using aDealerEDVMS.Repository.ToanHH.Models;

namespace aDealerEDVMS.Service.ToanHH
{

    public class SystemUserAccountService 
    {
        private readonly SystemUserAccountRepository _repository;
        public SystemUserAccountService()
        {
            _repository = new SystemUserAccountRepository();
        }
        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            try
            {
                return await _repository.GetUserAccountAsync(username, password);
            }
            catch (Exception ex)
            {
                // Log hoặc xử lý lỗi nếu cần
            }
            return null;
        }
            
    
    }
}

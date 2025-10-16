using aDealerEDVMS.Repository.ToanHH.Basic;
using aDealerEDVMS.Repository.ToanHH.DBcontext;
using aDealerEDVMS.Repository.ToanHH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aDealerEDVMS.Repository.ToanHH
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {


        public SystemUserAccountRepository() { }
        public SystemUserAccountRepository(FA25_PRN221_SE1834_G5_EVDMSContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccountAsync(string userName, string password)
            => await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true); // login bằng sdt và password và account phải kích hoạt

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true); // login bằng mã nhân viên và password và account phải kích hoạt

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true); // login bằng userName và password và account phải kích hoạt

    }
}

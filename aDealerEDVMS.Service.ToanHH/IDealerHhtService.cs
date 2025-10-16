using aDealerEDVMS.Repository.ToanHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aDealerEDVMS.Service.ToanHH
{
    public interface IDealerHhtService
    {
        Task<List<DealersHht>> GetAllAsync(); //implement trong model thôii
        Task<DealersHht> GetByIdAsync(int dealerId);
        Task<List<DealersHht>> SearchAsync(string DealerName, decimal Rating, string Address); //Hàm search theo tên đại lý
        Task<int> CreateAsync(DealersHht dealer);
        Task<int> UpdateAsync(DealersHht dealer);
        Task<bool> DeleteAsync(int dealerId);

    }
}

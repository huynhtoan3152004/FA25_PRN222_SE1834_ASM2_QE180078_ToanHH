using aDealerEDVMS.Repository.ToanHH.Basic;
using aDealerEDVMS.Repository.ToanHH.DBcontext;
using aDealerEDVMS.Repository.ToanHH.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aDealerEDVMS.Repository.ToanHH
{
    public class DealersHhtRepository : GenericRepository<DealersHht>
    {
        public DealersHhtRepository() : base() { }

        public DealersHhtRepository(FA25_PRN221_SE1834_G5_EVDMSContext context) : base(context) { }

        public async Task<List<DealersHht>> GetAllAsync()
        {
            var items = await _context.DealersHhts.ToListAsync();
            return items ?? new List<DealersHht>();
        }

        // Lấy đại lý theo DealerId
        public async Task<DealersHht> GetByIdAsync(int dealerId)
        {
            var dealer = await _context.DealersHhts.FirstOrDefaultAsync(d => d.DealerId == dealerId);
            return dealer ?? new DealersHht();
        }

        // Tìm kiếm đại lý theo tên
        public async Task<List<DealersHht>> SearchAsync(string dealerName, decimal rating, string address)
        {
            var items = await _context.DealersHhts
                .Where(d => d.DealerName.Contains(dealerName) || string.IsNullOrEmpty(dealerName))
                .ToListAsync();

            return items ?? new List<DealersHht>();
        }

        public async Task<int> CreateAsync(DealersHht dealer)
        {
            // ❌ KHÔNG làm: dealer.DealerId = 1;
            
            // ✅ Chỉ cần add vào context
            await _context.DealersHhts.AddAsync(dealer);
            
            // DealerId sẽ được database tự động generate sau khi SaveChanges
            return 1;
        }
    }
}

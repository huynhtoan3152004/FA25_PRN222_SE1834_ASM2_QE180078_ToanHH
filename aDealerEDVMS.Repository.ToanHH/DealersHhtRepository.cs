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
            return dealer;
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

        // ✅ THÊM METHOD UPDATE
        public async Task UpdateAsync(DealersHht dealer)
        {
            Console.WriteLine($"=== UpdateAsync in Repository ===");
            Console.WriteLine($"Updating dealer ID: {dealer.DealerId}");
            
            // Attach entity và mark as Modified
            var existingDealer = await _context.DealersHhts.FindAsync(dealer.DealerId);
            
            if (existingDealer != null)
            {
                // Update properties
                _context.Entry(existingDealer).CurrentValues.SetValues(dealer);
                Console.WriteLine("Entity marked as modified");
            }
            else
            {
                Console.WriteLine("Dealer not found in context, using Update method");
                _context.DealersHhts.Update(dealer);
            }
            
            // KHÔNG gọi SaveChanges ở đây - để UnitOfWork quản lý
        }

        // ✅ THÊM METHOD REMOVE
        public async Task RemoveAsync(DealersHht dealer)
        {
            Console.WriteLine($"=== RemoveAsync in Repository ===");
            Console.WriteLine($"Removing dealer ID: {dealer.DealerId}");
            
            // Remove from context
            _context.DealersHhts.Remove(dealer);
            
            Console.WriteLine("Entity marked for deletion");
            
            // KHÔNG gọi SaveChanges ở đây - để UnitOfWork quản lý
        }
    }
}

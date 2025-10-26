using aDealerEDVMS.Repository.ToanHH.Basic;
using aDealerEDVMS.Repository.ToanHH.DBcontext;
using aDealerEDVMS.Repository.ToanHH.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        // ✅ FIX UPDATE - Dùng AsNoTracking khi load, rồi Attach khi update
        public async Task UpdateAsync(DealersHht dealer)
        {
            Console.WriteLine($"=== UpdateAsync in Repository ===");
            Console.WriteLine($"Updating dealer ID: {dealer.DealerId}");
            
            try
            {
                // CÁCH 1: Detach entity cũ nếu có
                var local = _context.Set<DealersHht>()
                    .Local
                    .FirstOrDefault(e => e.DealerId == dealer.DealerId);

                if (local != null)
                {
                    Console.WriteLine("Detaching existing tracked entity...");
                    _context.Entry(local).State = EntityState.Detached;
                }

                // Attach entity mới và mark as Modified
                Console.WriteLine("Attaching and marking as Modified...");
                _context.DealersHhts.Attach(dealer);
                _context.Entry(dealer).State = EntityState.Modified;
                
                Console.WriteLine($"Entity state: {_context.Entry(dealer).State}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in UpdateAsync Repository: {ex.Message}");
                throw;
            }
        }

        // ✅ METHOD REMOVE
        public async Task RemoveAsync(DealersHht dealer)
        {
            Console.WriteLine($"=== RemoveAsync in Repository ===");
            Console.WriteLine($"Removing dealer ID: {dealer.DealerId}");
            
            _context.DealersHhts.Remove(dealer);
            
            Console.WriteLine("Entity marked for deletion");
        }
    }
}

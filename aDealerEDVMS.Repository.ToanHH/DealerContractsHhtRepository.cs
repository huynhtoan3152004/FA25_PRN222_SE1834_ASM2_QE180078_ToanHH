using Microsoft.EntityFrameworkCore;
using aDealerEDVMS.Repository.ToanHH.Basic;
using aDealerEDVMS.Repository.ToanHH.DBcontext;
using aDealerEDVMS.Repository.ToanHH.Models;

namespace aDealerEDVMS.Repository.ToanHH
{
    public class DealerContractsHhtRepository : GenericRepository<DealerContractsHht>
    {
        public DealerContractsHhtRepository() { }

        public DealerContractsHhtRepository(FA25_PRN221_SE1834_G5_EVDMSContext context) => _context = context;

        // Lấy tất cả hợp đồng, bao gồm thông tin đại lý
        public async Task<List<DealerContractsHht>> GetAllAsync()
        {
            var items = await _context.DealerContractsHhts
                .Include(dc => dc.Toandealer)
                .ToListAsync();

            return items ?? new List<DealerContractsHht>();
        }

        // Lấy hợp đồng theo ContractId, bao gồm thông tin đại lý
        public async Task<DealerContractsHht> GetByIdAsync(int contractId)
        {
            var contract = await _context.DealerContractsHhts
                .Include(dc => dc.Toandealer)
                .FirstOrDefaultAsync(dc => dc.ContractId == contractId);

            return contract ?? new DealerContractsHht();
        }

        // Tìm kiếm hợp đồng theo trạng thái và tên đại lý
        public async Task<List<DealerContractsHht>> SearchAsync(string status, string dealerName)
        {
            var items = await _context.DealerContractsHhts
                .Include(dc => dc.Toandealer)
                .Where(dc =>
                    (dc.Status.Contains(status) || string.IsNullOrEmpty(status)) &&
                    (dc.Toandealer != null && (dc.Toandealer.DealerName.Contains(dealerName) || string.IsNullOrEmpty(dealerName)))
                )
                .OrderByDescending(dc => dc.CreatedAt)
                .ToListAsync();

            return items ?? new List<DealerContractsHht>();
        }
    }
}
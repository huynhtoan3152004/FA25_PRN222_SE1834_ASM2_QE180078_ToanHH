using aDealerEDVMS.Repository.ToanHH.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aDealerEDVMS.Repository.ToanHH
{
    public interface IUnitOfWork : IDisposable
    {
        DealerContractsHhtRepository DealerContractsHhtRepository { get; }
        DealersHhtRepository DealersHhtRepository { get; }
        SystemUserAccountRepository SystemUserAccountRepository { get; }

        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FA25_PRN221_SE1834_G5_EVDMSContext _context;

        private DealerContractsHhtRepository _dealerContractsHhtRepository;

        private DealersHhtRepository _dealersHhtRepository;

        private SystemUserAccountRepository _systemUserAccountRepository;

        public UnitOfWork()
        {
            _context = new FA25_PRN221_SE1834_G5_EVDMSContext();
        }

        public DealersHhtRepository DealersHhtRepository
        {
            get { return _dealersHhtRepository ??= new DealersHhtRepository(_context); }
        }

        public DealerContractsHhtRepository DealerContractsHhtRepository
        {
            get { return _dealerContractsHhtRepository ??= new DealerContractsHhtRepository(_context); }
        }

        public SystemUserAccountRepository SystemUserAccountRepository
        {
            get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public int SaveChangesWithTransaction()
        {
            int result = 0;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit(); // FIX: Đây phải là await trong async
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Transaction error: {ex.Message}");
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync(); // FIX: Phải là CommitAsync
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Transaction error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
                    result = -1;
                    await dbContextTransaction.RollbackAsync(); // FIX: Phải là RollbackAsync
                }
            }

            return result;
        }
    }
}

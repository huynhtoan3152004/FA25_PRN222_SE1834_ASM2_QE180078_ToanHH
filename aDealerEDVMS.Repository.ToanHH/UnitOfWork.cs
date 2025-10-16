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
            throw new NotImplementedException();
        }

        public int SaveChangesWithTransaction()
        {
            int result = 0;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}

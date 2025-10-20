using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aDealerEDVMS.Service.ToanHH
{
    public interface IServiceProviders : IDisposable
    {
        SystemUserAccountService SystemUserAccountService { get; }
        DearlerHhtService DealersHhtService { get; }
        DealerContractsHhtService DealerContractsHhtService { get; }
    }

    public class ServiceProviders : IServiceProviders
    {
        private SystemUserAccountService _systemUserAccountService;
        private DearlerHhtService _dealersHhtService;
        private DealerContractsHhtService _dealerContractsHhtService;

        public SystemUserAccountService SystemUserAccountService
        {
            get { return _systemUserAccountService ??= new SystemUserAccountService(); }
        }

        public DearlerHhtService DealersHhtService
        {
            get { return _dealersHhtService ??= new DearlerHhtService(); }
        }

        public DealerContractsHhtService DealerContractsHhtService
        {
            get { return _dealerContractsHhtService ??= new DealerContractsHhtService(); }
        }

        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}

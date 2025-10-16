using aDealerEDVMS.Repository.ToanHH;
using aDealerEDVMS.Repository.ToanHH.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDealerEDVMS.Service.ToanHH
{
    public class DealerContractsHhtService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DealerContractsHhtService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<List<DealerContractsHht>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.DealerContractsHhtRepository.GetAllAsync();
            }
            catch (Exception)
            {
                return new List<DealerContractsHht>();
            }
        }
    }
}

using aDealerEDVMS.Repository.ToanHH;
using aDealerEDVMS.Repository.ToanHH.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDealerEDVMS.Service.ToanHH
{
    public class DearlerHhtService : IDealerHhtService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DearlerHhtService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<List<DealersHht>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.DealersHhtRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return new List<DealersHht>();
            }
        }

        public async Task<DealersHht> GetByIdAsync(int dealerId)
        {
            try
            {
                return await _unitOfWork.DealersHhtRepository.GetByIdAsync(dealerId);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return null;
            }
        }

        public async Task<int> CreateAsync(DealersHht dealer)
        {
            try
            {
                var result = await _unitOfWork.DealersHhtRepository.CreateAsync(dealer);
                if (result > 0)
                {
                    await _unitOfWork.SaveChangesWithTransactionAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi
                Console.WriteLine($"Error creating dealer: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            return 0;
        }

        public async Task<bool> DeleteAsync(int dealerId)
        {
            try
            {
                var dealer = await _unitOfWork.DealersHhtRepository.GetByIdAsync(dealerId);
                if (dealer != null)
                {
                    var removed = await _unitOfWork.DealersHhtRepository.RemoveAsync(dealer);
                    if (removed)
                    {
                        await _unitOfWork.SaveChangesWithTransactionAsync();
                    }
                    return removed;
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
            }
            return false;
        }

        public async Task<List<DealersHht>> SearchAsync(string dealerName, decimal rating, string address)
        {
            try
            {
                return await _unitOfWork.DealersHhtRepository.SearchAsync(dealerName, rating, address);
            }
            catch (Exception)
            {
                return new List<DealersHht>();
            }
        }

        public async Task<int> UpdateAsync(DealersHht dealer)
        {
            try
            {
                var updated = await _unitOfWork.DealersHhtRepository.UpdateAsync(dealer);
                if (updated > 0)
                {
                    await _unitOfWork.SaveChangesWithTransactionAsync();
                }
                return updated;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}

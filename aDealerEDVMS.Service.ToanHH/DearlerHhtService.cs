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
                Console.WriteLine($"Error getting all dealers: {ex.Message}");
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
                Console.WriteLine($"Error getting dealer by id: {ex.Message}");
                return null;
            }
        }

        public async Task<int> CreateAsync(DealersHht dealer)
        {
            try
            {
                Console.WriteLine("=== Starting CreateAsync ===");
                Console.WriteLine($"DealerCode: {dealer.DealerCode}");
                Console.WriteLine($"DealerName: {dealer.DealerName}");
                Console.WriteLine($"Address: {dealer.Address}");
                Console.WriteLine($"Phone: {dealer.Phone}");
                Console.WriteLine($"Email: {dealer.Email}");
                Console.WriteLine($"IsActive: {dealer.IsActive}");
                Console.WriteLine($"CreatedBy: {dealer.CreatedBy}");
                Console.WriteLine($"EstablishedDate: {dealer.EstablishedDate}");
                Console.WriteLine($"TotalStaff: {dealer.TotalStaff}");
                Console.WriteLine($"Rating: {dealer.Rating}");
                
                // FIX: Thêm vào repository trước
                await _unitOfWork.DealersHhtRepository.CreateAsync(dealer);
                
                Console.WriteLine("Added to repository, now saving...");
                
                // FIX: Sau đó mới save
                var result = await _unitOfWork.SaveChangesWithTransactionAsync();
                
                Console.WriteLine($"Save result: {result}");
                
                // FIX: Kiểm tra result từ SaveChanges (số record affected)
                if (result > 0)
                {
                    Console.WriteLine($"Dealer created successfully with ID: {dealer.DealerId}");
                    return dealer.DealerId;
                }
                
                Console.WriteLine("Failed to create dealer - no records affected");
                return 0;
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi
                Console.WriteLine($"=== ERROR in CreateAsync ===");
                Console.WriteLine($"Error message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception message: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                    
                    // Kiểm tra SqlException
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"Inner inner exception: {ex.InnerException.InnerException.Message}");
                    }
                }
                
                throw; // FIX: Throw lại exception để UI biết
            }
        }

        public async Task<int> UpdateAsync(DealersHht dealer)
        {
            try
            {
                Console.WriteLine("=== Starting UpdateAsync in Service ===");
                Console.WriteLine($"Dealer ID: {dealer.DealerId}");
                Console.WriteLine($"DealerCode: {dealer.DealerCode}");
                Console.WriteLine($"DealerName: {dealer.DealerName}");
                Console.WriteLine($"Address: {dealer.Address}");
                Console.WriteLine($"Phone: {dealer.Phone}");
                Console.WriteLine($"Email: {dealer.Email}");
                
                // Set LastAudit
                dealer.LastAudit = DateTime.Now;
                Console.WriteLine($"LastAudit set to: {dealer.LastAudit}");
                
                // Update dealer
                await _unitOfWork.DealersHhtRepository.UpdateAsync(dealer);
                
                Console.WriteLine("Dealer updated in repository, now saving...");
                
                // Save changes
                var result = await _unitOfWork.SaveChangesWithTransactionAsync();
                
                Console.WriteLine($"Save result: {result}");
                
                if (result > 0)
                {
                    Console.WriteLine($"Dealer updated successfully: {dealer.DealerId}");
                    return dealer.DealerId;
                }
                
                Console.WriteLine("Failed to update dealer - no records affected");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Error in UpdateAsync ===");
                Console.WriteLine($"Error message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"Inner inner exception: {ex.InnerException.InnerException.Message}");
                    }
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int dealerId)
        {
            try
            {
                Console.WriteLine("=== Starting DeleteAsync in Service ===");
                Console.WriteLine($"Dealer ID to delete: {dealerId}");
                
                // Get dealer first
                var dealer = await _unitOfWork.DealersHhtRepository.GetByIdAsync(dealerId);
                
                if (dealer == null)
                {
                    Console.WriteLine($"Dealer not found with ID: {dealerId}");
                    return false;
                }

                Console.WriteLine($"Dealer found: {dealer.DealerCode} - {dealer.DealerName}");
                
                // Remove dealer
                await _unitOfWork.DealersHhtRepository.RemoveAsync(dealer);
                
                Console.WriteLine("Dealer removed from repository, now saving...");
                
                // Save changes
                var result = await _unitOfWork.SaveChangesWithTransactionAsync();
                
                Console.WriteLine($"Save result: {result}");
                
                if (result > 0)
                {
                    Console.WriteLine($"Dealer deleted successfully: {dealerId}");
                    return true;
                }
                
                Console.WriteLine("Failed to delete dealer - no records affected");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Error in DeleteAsync ===");
                Console.WriteLine($"Error message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"Inner inner exception: {ex.InnerException.InnerException.Message}");
                    }
                }
                throw;
            }
        }

        public async Task<List<DealersHht>> SearchAsync(string dealerName, decimal rating, string address)
        {
            try
            {
                return await _unitOfWork.DealersHhtRepository.SearchAsync(dealerName, rating, address);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching dealers: {ex.Message}");
                return new List<DealersHht>();
            }
        }
    }
}

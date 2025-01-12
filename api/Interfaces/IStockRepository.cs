using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockDTO updateStockDTO);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);


    }
}
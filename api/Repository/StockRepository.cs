using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDTO updateStockDTO)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = updateStockDTO.Symbol;
            stock.CompanyName = updateStockDTO.CompanyName;
            stock.Purchase = updateStockDTO.Purchase;
            stock.LastDiv = updateStockDTO.LastDiv;
            stock.Industry = updateStockDTO.Industry;
            stock.MarketCap = updateStockDTO.MarketCap;

            await _context.SaveChangesAsync();

            return stock;
        }
    }
}
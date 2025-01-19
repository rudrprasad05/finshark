using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            
        }
        public async Task<List<Stock>> GetUserPortfoliosAsync(AppUser appUser)
        {
            return await _context.Portfolios.Where(x => x.AppUserId == appUser.Id)
                .Select(x => new Stock{
                    Id = x.StockId,
                    Symbol = x.Stock.Symbol,
                    MarketCap = x.Stock.MarketCap,
                    CompanyName = x.Stock.CompanyName,
                    Purchase = x.Stock.Purchase,
                    LastDiv = x.Stock.LastDiv,
                    Industry = x.Stock.Industry,
                 
                }).ToListAsync();
        }
    }
}
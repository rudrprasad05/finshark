using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext applicationDbContext, IStockRepository stockRepository)
        {
            _context = applicationDbContext;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stockDTO = stocks.Select(s => s.ToStockDTO());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO createStockDTO)
        {
            var stockModel = createStockDTO.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateStockDTO updateStockDTO)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null) return NotFound();

            stockModel.Symbol = updateStockDTO.Symbol;
            stockModel.CompanyName = updateStockDTO.CompanyName;
            stockModel.Purchase = updateStockDTO.Purchase;
            stockModel.LastDiv = updateStockDTO.LastDiv;
            stockModel.Industry = updateStockDTO.Industry;
            stockModel.MarketCap = updateStockDTO.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDTO());
        }
    }
}
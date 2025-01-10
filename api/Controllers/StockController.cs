using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
       
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDTO());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var stock = _context.Stocks.Find(id);

            if(stock == null){
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDTO createStockDTO){
            var stockModel = createStockDTO.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDTO());
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateStockDTO updateStockDTO)
        {   
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if(stockModel == null) return NotFound();

                stockModel.Symbol       = updateStockDTO.Symbol;
                stockModel.CompanyName  = updateStockDTO.CompanyName;
                stockModel.Purchase      = updateStockDTO.Purchase;
                stockModel.LastDiv      = updateStockDTO.LastDiv;
                stockModel.Industry     = updateStockDTO.Industry;
                stockModel.MarketCap       = updateStockDTO.MarketCap;
        }
    }
}
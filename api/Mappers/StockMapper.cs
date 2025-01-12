using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockModel){
           
            return new StockDTO{
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDTO()).ToList(),
            };
        }
         public static Stock ToStockFromCreateDTO(this CreateStockDTO dto){
            return new Stock{
                Symbol = dto.Symbol ?? string.Empty,
                CompanyName = dto.CompanyName ?? string.Empty,
                Purchase = dto.Purchase,
                LastDiv = dto.LastDiv,
                Industry = dto.Industry ?? string.Empty,
                MarketCap = dto.MarketCap,
            };
        }
    }

   
}

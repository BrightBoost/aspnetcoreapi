using Microsoft.AspNetCore.Mvc;
using QuoteCrudApi.Models;

namespace QuoteCrudApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController  : ControllerBase 
    {
        private static List<QuoteDto> quotes = new();

        [HttpGet]
        public IActionResult GetQuotes()
        {
            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuoteById(int id) 
        {
            QuoteDto quoteDto = quotes.FirstOrDefault(q => q.Id == id);
            return quoteDto == null ? NotFound() : Ok(quoteDto);
        }

        [HttpPost]
        public IActionResult CreateQuote([FromBody] QuoteDto quote) 
        {
            quote.Id = 1;
            quotes.Add(quote);
            return CreatedAtAction(nameof(GetQuoteById), new { id = quote.Id }, quote);
        }


    }
}
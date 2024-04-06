using ChainApiSample.Models.ProductTax;
using ChainApiSample.Models.TransactionSplit;
using Microsoft.AspNetCore.Mvc;
using SimpleChain;

namespace ChainApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly Chain<ProductModel> _productTaxChain;
        private readonly Chain<Transaction> _transactionChain;

        public TaxController(ILogger<TaxController> logger, Chain<ProductModel> productTaxChain, Chain<Transaction> transactionChain)
        {
            _logger = logger;
            _productTaxChain = productTaxChain;
            _transactionChain = transactionChain;
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(ProductModel product)
        {
            await _productTaxChain.ExecuteAsync(product);

            return Ok(product);
        }

        [HttpGet("SimulateSplit")]
        public async Task<IActionResult> Get()
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 100m
            };

            await _transactionChain.ExecuteAsync(transaction);

            return Ok(transaction);
        }

        [HttpGet("SimulateSplitLoad")]
        public async Task<IActionResult> LoadTest()
        {
            var count = 0;
            for (var i = 0; i < 100000; i++)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = Convert.ToDecimal(new Random().NextDouble() * 1000)
                };

                await _transactionChain.ExecuteAsync(transaction);
                count++;
            }

            return Ok(count);
        }
    }
}

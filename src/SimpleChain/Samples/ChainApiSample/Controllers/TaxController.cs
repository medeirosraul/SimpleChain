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
        private readonly ChainContainer<ProductModel> _productTaxChain;
        private readonly ChainContainer<Transaction> _transactionChain;

        public TaxController(ILogger<TaxController> logger, ChainContainer<ProductModel> productTaxChain, ChainContainer<Transaction> transactionChain)
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
    }
}

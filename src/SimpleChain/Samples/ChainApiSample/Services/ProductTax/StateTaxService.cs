using ChainApiSample.Models;
using ChainApiSample.Models.ProductTax;
using SimpleChain;

namespace ChainApiSample.Services.ProductTax
{
    public class StateTaxService : ChainHandler<ProductModel>
    {
        public override Task<ProductModel> HandleAsync(ProductModel input)
        {
            var taxPercentage = 0.05m;

            var tax = input.Price * taxPercentage;

            input.Taxes.Add(new TaxModel
            {
                Name = "State Tax",
                Value = tax
            });

            return Next(input);
        }
    }
}

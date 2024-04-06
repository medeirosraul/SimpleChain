using ChainApiSample.Models;
using ChainApiSample.Models.ProductTax;
using SimpleChain;

namespace ChainApiSample.Services.ProductTax
{
    public class MunicipalTaxService : ChainHandler<ProductModel>
    {
        public override Task<ProductModel> HandleAsync(ProductModel input)
        {
            var taxPercentage = 0.1m;

            var tax = input.Price * taxPercentage;

            input.Taxes.Add(new TaxModel
            {
                Name = "Municipal Tax",
                Value = tax
            });

            return Next(input);
        }
    }
}
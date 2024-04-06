namespace ChainApiSample.Models.ProductTax
{
    public class ProductModel
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public decimal FinalValue => Price + Taxes.Sum(t => t.Value);

        public List<TaxModel> Taxes { get; set; } = new List<TaxModel>();

    }
}

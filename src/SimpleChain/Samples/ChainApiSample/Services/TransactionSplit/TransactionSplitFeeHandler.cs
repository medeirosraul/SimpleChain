using ChainApiSample.Models.TransactionSplit;
using SimpleChain;

namespace ChainApiSample.Services.TransactionSplit
{
    public class TransactionSplitFeeHandler : ChainHandler<Split>
    {
        public override Task<Split> HandleAsync(Split input)
        {
            var taxPercentage = 0.05m;

            var tax = input.Amount * taxPercentage;

            input.Fees.Add(new TransactionFee
            {
                Description = "Split Fee",
                Value = tax
            });

            return Next(input);
        }
    }
}
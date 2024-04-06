using ChainApiSample.Models.TransactionSplit;
using SimpleChain;

namespace ChainApiSample.Services.TransactionSplit
{
    public class TransactionStateFeeHandler : ChainHandler<Transaction>
    {
        public override Task<Transaction> HandleAsync(Transaction input)
        {
            var taxPercentage = 0.05m;

            var tax = input.Amount * taxPercentage;

            input.Fees.Add(new TransactionFee
            {
                Description = "State Fee",
                Value = tax
            });

            return Next(input);
        }
    }
}
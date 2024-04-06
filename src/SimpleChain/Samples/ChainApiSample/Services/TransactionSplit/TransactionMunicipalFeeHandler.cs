using ChainApiSample.Models.TransactionSplit;
using SimpleChain;

namespace ChainApiSample.Services.TransactionSplit
{
    public class TransactionMunicipalFeeHandler : ChainHandler<Transaction>
    {
        public override Task<Transaction> HandleAsync(Transaction input)
        {
            var taxPercentage = 0.1m;

            var tax = input.Amount * taxPercentage;

            input.Fees.Add(new TransactionFee
            {
                Description = "Municipal Fee",
                Value = tax
            });

            return Next(input);
        }
    }
}
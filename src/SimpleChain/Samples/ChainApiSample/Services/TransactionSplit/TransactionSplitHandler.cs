using ChainApiSample.Models.TransactionSplit;
using SimpleChain;

namespace ChainApiSample.Services.TransactionSplit
{
    public class TransactionSplitHandler : ChainHandler<Transaction>
    {
        private readonly Chain<Split> _splitChain;

        public TransactionSplitHandler(Chain<Split> splitChain)
        {
            _splitChain = splitChain;
        }

        public override async Task<Transaction> HandleAsync(Transaction input)
        {
            var splitA = new Split
            {
                AccountNumber = "000001",
                Amount = input.NetAmount * 0.4m
            };

            var splitB = new Split
            {
                AccountNumber = "000002",
                Amount = input.NetAmount * 0.6m
            };

            await _splitChain.ExecuteAsync(splitA);
            await _splitChain.ExecuteAsync(splitB);

            input.Splits.Add(splitA);
            input.Splits.Add(splitB);

            return await Next(input);
        }
    }
}
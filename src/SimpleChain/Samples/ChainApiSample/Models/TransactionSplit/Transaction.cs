namespace ChainApiSample.Models.TransactionSplit
{
    public class Transaction
    {
        public required string Id { get; set; }
        public required decimal Amount { get; set; }
        public decimal TotalFee => Splits.Sum(s => s.Fees.Sum(x => x.Value)) + Fees.Sum(x => x.Value);
        public decimal NetAmount => Amount - TotalFee;

        public List<Split> Splits { get; } = new List<Split>();
        public List<TransactionFee> Fees { get; } = new List<TransactionFee>();
    }
}

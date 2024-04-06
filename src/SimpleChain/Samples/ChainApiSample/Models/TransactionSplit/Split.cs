namespace ChainApiSample.Models.TransactionSplit
{
    public class Split
    {
        public required string AccountNumber { get; set; }
        public required decimal Amount { get; set; }
        public decimal FinalValue => Amount - Fees.Sum(f => f.Value);
        public List<TransactionFee> Fees { get; } = new List<TransactionFee>();
    }
}

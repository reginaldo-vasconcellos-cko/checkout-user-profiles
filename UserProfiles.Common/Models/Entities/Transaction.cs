using System;

namespace UserProfiles.Common.Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public Merchant Merchant { get; set; }

        public Business Business { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}

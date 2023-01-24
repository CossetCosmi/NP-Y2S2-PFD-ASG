using System;

namespace PFD.Model
{
    internal class Transaction : IModel<int>
    {
        public int Id { get; set; }
        public string Card { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateOn { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public int ATM { get; set; }
    }
}

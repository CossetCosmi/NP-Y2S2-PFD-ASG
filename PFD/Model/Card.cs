using System;

namespace PFD.Model
{
    internal class Card : IModel<string>
    {
        public string Id { get; set; }
        public int AccountId { get; set; }
        public string CVV { get; set; }
        public DateTime IssueOn { get; set; }
        public DateTime ExpireOn { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public string Pin { get; set; }
    }
}

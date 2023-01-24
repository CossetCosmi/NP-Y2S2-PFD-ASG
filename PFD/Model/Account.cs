using PFD.Model.Interface;

namespace PFD.Model
{
    internal sealed class Account : IModel<int>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

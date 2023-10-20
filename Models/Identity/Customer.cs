using Microsoft.AspNetCore.Identity;

namespace QT1_WEB.Models.Identity
{
    public class Customer : IdentityUser
    {
        public string CustId { get; set; } = null!;

        public string? CustName { get; set; }

        public string? Address { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}


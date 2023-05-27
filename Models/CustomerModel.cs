using System;
namespace Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}


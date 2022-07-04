using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Booking = new HashSet<Booking>();
            Feedback = new HashSet<Feedback>();
        }

        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Passport { get; set; }
        public byte[] DriversLicense { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}

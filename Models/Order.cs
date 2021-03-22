using System;
using System.ComponentModel;

namespace Models
{
    public class Order : Entity
    {
        public User User { get; set; }

        public IEquatable<Product> Products { get; set; }
    }
}

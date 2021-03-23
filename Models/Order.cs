using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Models
{
    public class Order : Entity
    {
        public User User { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}

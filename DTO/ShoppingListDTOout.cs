using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ShoppingListDTOout
    {
        public string Name { get; set; }
        public bool Delivered { get; set; }
        public string Shop { get; set; }
        public string Owner { get; set; }

        public double TotalPrice { get; set; }

        public int NbItems { get; set; }
        public string DelivererName { get; set; }
    }
}

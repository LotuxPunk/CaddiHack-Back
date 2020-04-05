using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace DTO
{
    public class ShoppingListDTOout
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public bool Delivered { get; set; }
        public string Shop { get; set; }
        public string Owner { get; set; }

        public double TotalPrice { get; set; }

        public int NbItems { get; set; }
        public string DelivererName { get; set; }

        public List<Item> Items { get; set; }
    }
}

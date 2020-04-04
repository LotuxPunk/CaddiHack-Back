using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ShoppingListDTOin
    {
        public int ShopId { get; set; }
        public string ListName { get; set; }
        public List<ItemDTOin> Items { get; set; }
    }
}

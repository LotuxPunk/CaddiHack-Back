using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ShopDTOout
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
        public string LocalityName { get; set; }
        public bool IsFavorite {get; set;}

    }
}

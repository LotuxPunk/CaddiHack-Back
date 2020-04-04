using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ShopDTOin
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Locality { get; set; }

        public IFormFile Picture { get; set; }
    }
}

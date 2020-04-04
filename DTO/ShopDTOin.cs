using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ShopDTOin
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Locality { get; set; }

        public string Description { get; set; }

        public IFormFile Picture { get; set; }
    }
}

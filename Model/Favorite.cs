using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Favorite
    {
        public int FavoriteId { get; set; }
        public int Person { get; set; }
        public int Shop { get; set; }

        public virtual Person PersonNavigation { get; set; }
        public virtual Shop ShopNavigation { get; set; }
    }
}

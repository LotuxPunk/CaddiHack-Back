using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class PersonRole
    {
        public int Person { get; set; }
        public string Role { get; set; }

        public virtual Person PersonNavigation { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}

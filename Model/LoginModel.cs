using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public partial class LoginModel
    {
     
        public String Email { get; set;}
        public String Password { get; set; }
    }
}

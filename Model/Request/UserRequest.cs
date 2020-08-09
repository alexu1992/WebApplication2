using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Request
{
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Models
{
    public class LoginViewModel
    {
        [Required, StringLength(50, ErrorMessage = "Email character limit is between 3 and 50", MinimumLength = 10)]
        public string Email { get; set; }

        [Required, StringLength(38, ErrorMessage = "Password charater limit is between 8 and 38", MinimumLength = 8)]
        public string Password { get; set; }
    }
}

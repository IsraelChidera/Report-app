using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Models
{
    public class UserViewModel
    {
        [Required, StringLength(50, ErrorMessage = "Fullname character limit is between 3 and 50", MinimumLength = 3)]
        public string FullName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Email character limit is between 3 and 50", MinimumLength = 10)]
        public string Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "Title character limit is between 3 and 50", MinimumLength = 3)]
        public string Title { get; set; }

        [Required, StringLength(38, ErrorMessage = "Password charater limit is between 8 and 38", MinimumLength = 8)]
        public string Password { get; set; }

        [Required, StringLength(38, ErrorMessage = "Confirm Password charater limit is between 8 and 38", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }


    }
}

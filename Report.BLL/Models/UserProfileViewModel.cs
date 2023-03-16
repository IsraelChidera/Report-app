using Report.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Models
{
    public class UserProfileViewModel
    {
        [Required, StringLength(50, ErrorMessage = "Fullname character limit is between 3 and 50", MinimumLength = 10)]
        public string FullName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Email character limit is between 3 and 50", MinimumLength = 10)]
        public string Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "Fullname character limit is between 3 and 50", MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        public string Password { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}

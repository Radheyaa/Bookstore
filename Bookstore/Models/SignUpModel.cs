using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class SignUpModel
    {

        [Required(ErrorMessage = "Please enter your FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Compare("ConfirmPassword", ErrorMessage ="Password don't match")]
        [DataType(DataType.Password)]
        public string   Password { get; set; }

        [Required(ErrorMessage = "Please cofirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Passwsord")]
        public string ConfirmPassword { get; set; }

    }
}

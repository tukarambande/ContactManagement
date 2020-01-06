using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Contact Id")]
        [ScaffoldColumn(false)]
        public int ContactId { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression(@"\w+$", ErrorMessage = "Only Alpanumeric name is accepted (No special characters allowed).")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"\w+$", ErrorMessage = "Only Alpanumeric name is accepted (No special characters allowed).")]
        public string LastName { get; set; }

        [Display(Name = "Email-Id")]
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter the correct Email Id.")]
        public string EmailId { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"\d+$", ErrorMessage = "Only numeric value is accepted (No alpabets and special characters allowed).")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        [Display(Name = "Address")]
        [StringLength(maximumLength: 128, MinimumLength = 10, ErrorMessage = "Minimum 10 and Maximum 128 Characters are allowed.")]
        public string Address { get; set; }
    }
}

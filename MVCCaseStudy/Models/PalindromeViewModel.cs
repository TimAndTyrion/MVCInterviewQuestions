using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCCaseStudy.Models
{
    public class PalindromeViewModel
    {
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must enter in a positive intiger.")]
        [Display(Name = "Orginal Number:")]
        public string OriginalNumber { get; set; }

        [Display(Name = "ReverseString")]
        public string ReverseString { get; set; }

        public bool IsPalindrome { get; set; }

        public bool DisplayResult { get; set; }

        public string Message { get; set; }
    }
}
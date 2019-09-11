using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCCaseStudy.Models
{
    public class MoneyConversionViewModel
    {
        [Required]
        [RegularExpression(@"^-?\d{1,3}(?:(,?)\d{3}(?:\1\d{3})*)?(?:\.\d{2})?$", ErrorMessage = "Enter a value in the format of XXX,XXX.00")]
        [Display(Name = "Money Decimal Amount:")]
        public string InputMoneyString { get; set; }

        [Display(Name = "Money String Output")]
        public string MoneyStringFromat { get; set; }

    }
   
}

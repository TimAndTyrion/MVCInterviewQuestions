using System;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCCaseStudy.Models;

namespace MVCCaseStudy.Controllers
{
    public class MoneyConversionController : Controller
    {      

        [AllowAnonymous]
        public ActionResult Input()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Output(MoneyConversionViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Input(MoneyConversionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            viewModel.MoneyStringFromat = GetMoneyStringOutput(viewModel.InputMoneyString);
            return RedirectToAction("Output", viewModel);
        }

        public string GetMoneyStringOutput(string inputMoneyString)
        {
            var dollarValueIntoString = NumericStringToWords(inputMoneyString);
            var fractionStringAndDollars = DisplayFractionAndDollars(inputMoneyString);
            return String.Format("{0} and {1}", dollarValueIntoString, fractionStringAndDollars);
        }

        private string DisplayFractionAndDollars(string moneyStringInput)
        {
            var fraction = string.Empty;
            string[] split = moneyStringInput.Split('.');
            if (split.Length > 1)
            {
                return String.Format("{0}/100 Dollars", split[1]);
            }            
            return "00/100 Dollars";
        }

        private string NumericStringToWords(string moneyStringInput)
        {
            string[] split = moneyStringInput.Split('.');
            var numericStringValue = split[0].Replace(",", "");

            string[] units = { "One", "Two", "Three", "Four", "Five",
                           "Six", "Seven", "Eight", "Nine" };

            string[] teens = { "Eleven", "Twelve", "Thirteen", "Four", "Fifteen",
                           "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty",
                          "Sixty", "Seventy", "Eighty", "Ninety" };

            string[] thou = { "Thousand", "Million", "Billion", "Trillion",
                          "Quadrillion", "Quintillion", "Sextillion",
                          "Septillion", "Octillion"};

            int numericValue;
            string correctNumericString = string.Empty;
            if (Int32.TryParse(numericStringValue, out numericValue))
            {
                if (numericValue == 0)
                {
                    return "Zero";
                }
                correctNumericString = numericValue.ToString();
            }

            StringBuilder moneyWord = new StringBuilder();
            int loopCount = 0;
            var previousBlock = string.Empty;
            while (0 < correctNumericString.Length)
            {
                int startPos = Math.Max(0, correctNumericString.Length - 3);
                string currentBlock = correctNumericString.Substring(startPos);
                if (0 < currentBlock.Length)
                {
                    int h = currentBlock.Length > 2 ? int.Parse(currentBlock[currentBlock.Length - 3].ToString()) : 0;
                    int t = currentBlock.Length > 1 ? int.Parse(currentBlock[currentBlock.Length - 2].ToString()) : 0;
                    int u = currentBlock.Length > 0 ? int.Parse(currentBlock[currentBlock.Length - 1].ToString()) : 0;

                    StringBuilder thisBlock = new StringBuilder();

                    if (0 < u)
                        thisBlock.Append(1 == t ? teens[u - 1] : units[u - 1]);

                    if (1 != t)
                    {
                        if (1 < t && 0 < u) thisBlock.Insert(0, "-");
                        if (0 < t) thisBlock.Insert(0, tens[t - 1]);
                    }

                    if (0 < h)
                    {
                        if (t > 0 | u > 0) thisBlock.Insert(0, " and ");
                        thisBlock.Insert(0, String.Format("{0} Hundred", units[h - 1]));
                    }

                    bool MoreLeft = 3 < correctNumericString.Length;

                    if (!MoreLeft && 0 == u && 0 == h && t != 0 && 0 == loopCount)
                    {
                        thisBlock.Insert(0, String.Format(" {0}", tens[t - 1]));
                    }
                    if (MoreLeft && 0 == h && 0 == loopCount && currentBlock != "000")
                    {
                        thisBlock.Insert(0, " and ");
                        previousBlock = currentBlock;
                    }
                    else if (MoreLeft && currentBlock != "000")
                    {
                        thisBlock.Insert(0, String.Format(" {0}, ", thou[loopCount]));
                    }
                    else if (MoreLeft && currentBlock == "000")
                    {
                        thisBlock.Insert(0, String.Format(" {0}", thou[loopCount]));
                    }
                    else if (MoreLeft)
                    {
                        thisBlock.Insert(0, String.Format(" {0},", thou[loopCount]));
                    }
                   

                    moneyWord.Insert(0, thisBlock);
                }
                correctNumericString = correctNumericString.Substring(0, startPos);

                loopCount++;
            }
            return moneyWord.ToString();
        }
    }
}
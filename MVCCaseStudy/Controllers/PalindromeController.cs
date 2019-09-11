using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCaseStudy.Models;

namespace MVCCaseStudy.Controllers
{
    public class PalindromeController : Controller
    {
        // GET: Palindrome
        public ActionResult Index()
        {
            return View(new PalindromeViewModel { DisplayResult = false });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PalindromeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DisplayResult = false;
                return View(viewModel);
            }
            viewModel.DisplayResult = true;
            viewModel.ReverseString = GetReverseNumber(viewModel.OriginalNumber);
            viewModel.IsPalindrome = (viewModel.OriginalNumber == viewModel.ReverseString);
            return View(viewModel);
        }

        private string GetReverseNumber(string orginalNumber)
        {
            char[] charArray = orginalNumber.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
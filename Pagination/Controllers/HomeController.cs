using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagination.Models;

namespace Pagination.Controllers
{
    public class HomeController : Controller
    {
        List<Product> products = new List<Product>();

        decimal pageSize = 4; // maximum of products displayed per page
        decimal page = 1;
        decimal skipSize = 0;
        decimal numberOfPages = 0;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products(decimal? pageNumber)
        {

            page = pageNumber == null ? 1 : (decimal) pageNumber;

            // Populate some products for the store
            PopulateProducts();

            // Get the number of products
            int numberOfProducts = products.Count();

            // Decide which text is on the site related to the number of products
            switch (numberOfProducts)
            {
                case 0:
                    ViewBag.NumberOfProducts = "Oh nooo! There are no in store :(";
                  break;
                case 1:
                    ViewBag.NumberOfProducts = "There is only 1 product in store";
                    break;
                default:
                    if (numberOfProducts > 1)
                    {
                        ViewBag.NumberOfProducts = "There are " + products.Count() + " products in store";
                    }
                    break;
            }

            // Set the number of pages for pagination
            numberOfPages = Math.Ceiling(products.Count() / pageSize);
            ViewBag.NumberOfPages = numberOfPages;

            skipSize = (page * pageSize) - pageSize;

            return View(products.Skip((int) skipSize).Take((int) pageSize));
        }

        /// <summary>
        /// Populate some fictional products for the store
        /// </summary>
        private void PopulateProducts()
        {
            for (int i = 1; i <= 10; i++)
            {
                Product p = new Product()
                { Name = "product"+i };

                products.Add(p);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

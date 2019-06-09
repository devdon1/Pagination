using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagination.Models;

namespace Pagination.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>();

        decimal pageSize = 5; // maximum of products displayed per page
        decimal page = 1;
        decimal skipSize = 0;
        decimal numberOfPages = 0;

        public IActionResult Index(decimal? pageNumber, int productsPerPage)
        {
            page = pageNumber == null ? 1 : (decimal)pageNumber;

            // Set the pageSize based on the selected products per page
            // After initial load of the page, the pageSize is set at 5
            pageSize = productsPerPage == 0 ? 5 : productsPerPage;

            ViewBag.producstsPersPage = pageSize;

            // Populate some products for the store
            PopulateProducts();

            // Get the number of products
            int numberOfProducts = products.Count();

            // Store the number of products in a Viewbag
            ViewBag.ProductCount = numberOfProducts;

            // Decide which text is displayed on the page related to the number of products (how many in stock)
            switch (numberOfProducts)
            {
                case 0:
                    ViewBag.NumberOfProducts = "Oh nooo! There are no products in store :(";
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

            // Set the number of pages for pagination based on number of products and pageSize
            // And store the number of pages in a Viewbag
            numberOfPages = Math.Ceiling(products.Count() / pageSize);
            ViewBag.NumberOfPages = numberOfPages;

            // Determine the skipSize (how many items in the list should be skipped)
            skipSize = (page * pageSize) - pageSize;

            // Return the list with products combined with skip and Take (pageSize).
            return View(products.Skip((int)skipSize).Take((int)pageSize));
        }

        /// <summary>
        /// Populate some fictional products for the store
        /// </summary>
        private void PopulateProducts()
        {
            for (int i = 1; i <= 30; i++)
            {
                Product p = new Product()
                { Name = "product" + i };

                products.Add(p);
            }
        }

        [HttpPost]
        public IActionResult NumberOfProducts(string value)
        {
            //Products(null, Int32.Parse(value));
            return RedirectToAction("Index", new { value });
        }
    }
}
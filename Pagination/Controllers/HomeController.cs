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

        decimal pageSize = 4;
        decimal page = 1;
        decimal skipSize = 0;
        decimal numberOfPages = 0;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products(decimal? pageNumber)
        {
            page = pageNumber == null ? 1 : (decimal) pageNumber;

            PopulateProducts();

            numberOfPages = Math.Ceiling(products.Count() / pageSize);
            ViewBag.NumberOfPages = numberOfPages;

            skipSize = (page * pageSize) - pageSize;

            return View(products.Skip((int) skipSize).Take((int) pageSize));
        }

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

using Adapters;
using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace Northwind.WebApi.Host.Controllers
{
    public class ProductsController : Controller
    {
        ProductsAdapter productsAdapter = new ProductsAdapter();
        // GET: Products
        public ActionResult Index()
        {
            var result = this.productsAdapter.SendRequest<IEnumerable<Products>>("GetProducts");
            if (result.ReturnCode == "0000")
            {
                return View(result.Result);
            }
            return View();
        }
    }
}
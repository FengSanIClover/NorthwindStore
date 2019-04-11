using Northwind.Modules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Northwind.WebApi.Host.Controllers.Apis
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        IProducts productsService;
        public ProductsController(IProducts productsService)
        {
            this.productsService = productsService;
        }

        [Route("GetProducts")]
        public async Task<IHttpActionResult> GetProducts()
        {
            return Ok(await this.productsService.Query().SelectAsync());
        }
    }
}

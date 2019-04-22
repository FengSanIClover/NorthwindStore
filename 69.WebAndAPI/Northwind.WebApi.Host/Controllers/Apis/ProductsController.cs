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
    public class ProductsController : BaseApiController
    {
        IProducts productsService;
        public ProductsController(IProducts productsService)
        {
            this.productsService = productsService;
        }

        [Route("GetProducts")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProducts()
        {
            return Success(await this.productsService.Query().SelectAsync());
        }
    }
}

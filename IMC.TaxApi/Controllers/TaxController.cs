using ICM.TaxApi.Models.Domain;
using IMC.TaxApi.Core.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMC.TaxApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly ITaxProvider _taxProvider;

        public TaxController(ILogger<TaxController> logger,
                             ITaxProvider taxProvider)
        {
            _logger = logger;
            _taxProvider = taxProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("rate")]
        [HttpGet]
        public async Task<IActionResult> GetTaxRates([FromQuery] GetTaxRateRequest request)
        {
            var getTaxRateResponse = await _taxProvider.GetTaxRates(request);
            return Ok(getTaxRateResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ordersaletax")]
        [HttpPost]
        public async Task<IActionResult> GetOrderSalesTax([FromBody] GetOrderSalesTaxRequestModel getOrderSalesTaxRequest)
        {
            var getOrderSalesTaxResponse = await _taxProvider.GetOrderSalesTax(getOrderSalesTaxRequest);
            return Ok(getOrderSalesTaxResponse);  
        }
    }
}

using ICM.TaxApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMC.TaxApi.Core.Providers
{
    public interface ITaxProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getTaxRateRequest"></param>
        /// <returns></returns>
        Task<GetTaxRateResponse> GetTaxRates(GetTaxRateRequest getTaxRateRequest);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderTax"></param>
        /// <returns></returns>
        Task<GetOrderSalesTaxResponse> GetOrderSalesTax(GetOrderSalesTaxRequestModel getOrderSalesTaxRequest);
    }
}

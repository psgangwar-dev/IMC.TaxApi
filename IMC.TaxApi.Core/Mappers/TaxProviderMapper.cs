using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.TaxApi.Core.Mappers
{
    public class TaxProviderMapper : ITaxProviderMapper
    {
        /// <summary>
        /// Mapper method to map the GetTaxRates Partner Uri
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string MapGetTaxRatesUri(GetTaxRateRequest request)
        {
            var getTaxRatesUri = $"v2/rates/{request.Zip}";
            var uriSb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(request.Street))
                uriSb.Append($"&street{request.Street}");

            if (!string.IsNullOrWhiteSpace(request.City))
                uriSb.Append($"&city{request.City}");

            if (!string.IsNullOrWhiteSpace(request.State))
                uriSb.Append($"&state{request.State}");

            if (!string.IsNullOrWhiteSpace(request.Country))
                uriSb.Append($"&country{request.Country}");

            return uriSb.Length > 0 ? $"{getTaxRatesUri}?{uriSb.ToString()}" : getTaxRatesUri;
        }

        /// <summary>
        /// Mapper method for GetOrderSalesTax Request
        /// </summary>
        /// <param name="getOrderSalesTaxRequestModel"></param>
        /// <returns></returns>
        public GetOrderSalesTaxRequest MapGetOrderSalesTaxRequest(GetOrderSalesTaxRequestModel getOrderSalesTaxRequestModel)
        {
            return new GetOrderSalesTaxRequest()
            {
                // Manadatory fields
                To_country = getOrderSalesTaxRequestModel.To_country,
                Shipping = getOrderSalesTaxRequestModel.Shipping,
                // Manadatory fields - END
                From_country = getOrderSalesTaxRequestModel.From_country,
                To_zip = getOrderSalesTaxRequestModel.To_zip,
                To_state = getOrderSalesTaxRequestModel.To_state,
                From_zip = getOrderSalesTaxRequestModel.From_zip,
                From_city = getOrderSalesTaxRequestModel.From_city,
                From_state = getOrderSalesTaxRequestModel.From_state,
                From_street = getOrderSalesTaxRequestModel.From_street,
                To_city = getOrderSalesTaxRequestModel.To_city,
                To_street = getOrderSalesTaxRequestModel.To_street,
                Amount = getOrderSalesTaxRequestModel.Amount,
                Nexus_addresses = getOrderSalesTaxRequestModel.Nexus_addresses,
                Line_items = getOrderSalesTaxRequestModel.Line_items,
            };
        }
    }
}

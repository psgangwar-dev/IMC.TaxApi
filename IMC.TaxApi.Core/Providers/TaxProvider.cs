using ICM.TaxApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMC.TaxApi.Core.Repository;
using IMC.TaxApi.Core.Validators;
using ICM.TaxApi.Models.Entities;

namespace IMC.TaxApi.Core.Providers
{
    public class TaxProvider : ITaxProvider
    {
        private readonly ILogger<TaxProvider> _taxProviderLogger;
        private readonly ITaxRepository _taxRepository;
        private readonly ITaxProviderValidator _taxProviderValidator;

        public TaxProvider(ILogger<TaxProvider> logger,
                           ITaxRepository taxRepository,
                           ITaxProviderValidator taxProviderValidator)
        {
            _taxRepository = taxRepository;
            //_userMapper = userMapper;
            _taxProviderLogger = logger;
            _taxProviderValidator = taxProviderValidator;
        }

        public async Task<GetTaxRateResponse> GetTaxRates(GetTaxRateRequest getTaxRateRequest)
        {
            // Validate Request Data
            _taxProviderValidator.ValidateGetTaxRateRequest(getTaxRateRequest);

            // Map Downstream Api request
            // var TaxRateResponseRequest = new TaxRateResponseApiRequest() { Zip = zip, City = city, Country = country, State = state, Street = street };

            // Process Request
            var response = await _taxRepository.GetTaxRateResponse($"v2/rates/{33716}");

            // Return Response
            return response;
        }

        public async Task<GetOrderSalesTaxResponse> GetOrderSalesTax(GetOrderSalesTaxRequestModel getOrderSalesTaxRequest)
        {
            var apiRequest = new GetOrderSalesTaxRequest()
            {
                //required
                To_country = getOrderSalesTaxRequest.To_country,
                Shipping = getOrderSalesTaxRequest.Shipping,
                From_country = getOrderSalesTaxRequest.From_country,
                ////optional
                //From_country = orderTax.From_country,
                //From_zip = orderTax.From_zip,
                //From_city = orderTax.From_city,
                //From_state = orderTax.From_state,
                //From_street = orderTax.From_street,

                //To_city = orderTax.To_city,
                //To_street = orderTax.To_street,
                //Amount = orderTax.Amount,
                ////Nexus_addresses = orderTax.Nexus_addresses,
                ////Line_items = orderTax.Line_items,

                ////conditional
                To_zip = getOrderSalesTaxRequest.To_zip,
                //To_state = orderTax.To_state,
            };

            var response = await _taxRepository.TaxForOrder(apiRequest);
            return response;
        }
    }
}

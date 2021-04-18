using ICM.TaxApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMC.TaxApi.Core.Repository;
using IMC.TaxApi.Core.Validators;
using ICM.TaxApi.Models.Entities;
using IMC.TaxApi.Core.Mappers;

namespace IMC.TaxApi.Core.Providers
{
    public class TaxProvider : ITaxProvider
    {
        #region Ctro and Class Level variables

        private readonly ILogger<TaxProvider> _taxProviderLogger;
        private readonly ITaxRepository _taxRepository;
        private readonly ITaxProviderValidator _taxProviderValidator;
        private readonly ITaxProviderMapper _taxProviderMapper;

        public TaxProvider(ILogger<TaxProvider> logger,
                           ITaxRepository taxRepository,
                           ITaxProviderValidator taxProviderValidator,
                           ITaxProviderMapper taxProviderMapper)
        {
            _taxRepository = taxRepository;
            _taxProviderLogger = logger;
            _taxProviderValidator = taxProviderValidator;
            _taxProviderMapper = taxProviderMapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///  This method is used to get the sales tax rates for a given location.
        ///  Calls TaxRepository.GetTaxRateResponse, which in turn calls TaxJar API to fetch the required details  
        /// </summary>
        /// <param name="getTaxRateRequest"></param>
        /// <returns>GetTaxRateResponse</returns>
        public async Task<GetTaxRateResponse> GetTaxRates(GetTaxRateRequest getTaxRateRequest)
        {
            // Validate Request Data
            _taxProviderValidator.ValidateGetTaxRateRequest(getTaxRateRequest);

            // Map Downstream Api request
            var getTaxRatesPartnerUri = _taxProviderMapper.MapGetTaxRatesUri(getTaxRateRequest);

            // Process Request
            var response = await _taxRepository.GetTaxRateResponse(getTaxRatesPartnerUri);

            // Return Response
            return response;
        }

        /// <summary>
        ///  This method is used to fetch the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="getOrderSalesTaxRequest"></param>
        /// <returns>GetOrderSalesTaxResponse</returns>
        public async Task<GetOrderSalesTaxResponse> GetOrderSalesTax(GetOrderSalesTaxRequestModel getOrderSalesTaxRequest)
        {
            // Map Partner Request
            var apiRequest = _taxProviderMapper.MapGetOrderSalesTaxRequest(getOrderSalesTaxRequest);

            // Process Request
            var response = await _taxRepository.GetOrderSalesTax(apiRequest);

            // Return Response
            return response;
        }

        #endregion
    }
}

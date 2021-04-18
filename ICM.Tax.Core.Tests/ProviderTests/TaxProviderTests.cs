using IMC.TaxApi.Core.Validators;
using System;
using Xunit;
using IMC.TaxApi.Core.Repository;
using IMC.TaxApi.Core.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using ICM.TaxApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using ICM.TaxApi.Models.Entities;

namespace ICM.Tax.Core.Tests
{
    public class TaxProviderTests
    {
        private Mock<ILogger<TaxProvider>> _taxProviderLogger;
        private Mock<ITaxRepository> _taxRepository;
        private Mock<ITaxProviderValidator> _taxProviderValidator;
        private ITaxProvider _taxProvider;
        public TaxProviderTests()
        {
            _taxProviderLogger = new Mock<ILogger<TaxProvider>>();
            _taxRepository = new Mock<ITaxRepository>();
            _taxProviderValidator = new Mock<ITaxProviderValidator>();
            _taxProvider = new TaxProvider(_taxProviderLogger.Object, _taxRepository.Object, _taxProviderValidator.Object);
        }

        [Fact]
        public async void GetTaxRates_ValidData_Expect_Success()
        {
            // Setup 
            var mockResponse = new GetTaxRateResponse()
            {
                Rate = new TaxRate
                {
                    City = "Saint Pete",
                    Zip = "33701",
                    State_rate = "10.99"
                }
            };
            _taxRepository.Setup(x => x.GetTaxRateResponse(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();


            _taxProviderValidator.Setup(x=> x.ValidateGetTaxRateRequest(It.IsAny<GetTaxRateRequest>()));

            // Act 
            var actualResponse = await _taxProvider.GetTaxRates(It.IsAny<GetTaxRateRequest>()) as GetTaxRateResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.Equal("Saint Pete", actualResponse.Rate.City);
            Assert.Equal("33701", actualResponse.Rate.Zip);
            Assert.Equal("10.99", actualResponse.Rate.State_rate);
        }

        [Theory]
        [InlineData(19, "USA", "AU")]
        public async void GetOrderSalesTax_ValidInput_Expect_Success(int amount, string fromCountry, string toCountry)
        {
            // Setup 
            var mockResponse = new GetOrderSalesTaxResponse()
            {
                SalesTax = new SalesTax
                {
                    amount_to_collect = 12.90,
                    Order_total_amount = 124.88,
                    breakdown = new Breakdown
                    {
                        city_taxable_amount = 78,
                        city_tax_rate = 7
                    },
                    jurisdictions = new Jurisdictions
                    {
                        city = "Saint Pete",
                        country = "US"
                    }
                }
            };
            var fakeRequest = new GetOrderSalesTaxRequestModel
            {
                Amount = amount,
                From_country = fromCountry,
                To_country = toCountry
            };

            _taxRepository.Setup(x => x.TaxForOrder(It.IsAny<GetOrderSalesTaxRequest>()))
                 .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act 
            var actualResponse = await _taxProvider.GetOrderSalesTax(fakeRequest) as GetOrderSalesTaxResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(actualResponse.SalesTax);
            Assert.Equal(12.90, actualResponse.SalesTax.amount_to_collect);
            Assert.Equal(124.88, actualResponse.SalesTax.Order_total_amount);
            Assert.NotNull(actualResponse.SalesTax.breakdown);
            Assert.Equal(78, actualResponse.SalesTax.breakdown.city_taxable_amount);
            Assert.NotNull(actualResponse.SalesTax.jurisdictions);
            Assert.Equal("Saint Pete", actualResponse.SalesTax.jurisdictions.city);
        }
    }
}

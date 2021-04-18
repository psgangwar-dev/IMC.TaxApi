using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using IMC.TaxApi.Controllers;
using IMC.TaxApi.Core.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IMC.Tax.Host.Tests
{
    public class TaxControllerTests
    {
        private Mock<ILogger<TaxController>> _logger;
        private Mock<ITaxProvider> _taxProvider;
        private TaxController _taxController;

        public TaxControllerTests()
        {
            _logger = new Mock<ILogger<TaxController>>();
            _taxProvider = new Mock<ITaxProvider>();
            _taxController = new TaxController(_logger.Object, _taxProvider.Object);
        }

        [Fact]
        public async void GetTaxRates_ValidInput_Expect_Success()
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
            _taxProvider.Setup(x => x.GetTaxRates(It.IsAny<GetTaxRateRequest>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act 
            var actualResponse = await _taxController.GetTaxRates(It.IsAny<GetTaxRateRequest>()) as OkObjectResult;
            var data = actualResponse.Value as GetTaxRateResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(data);
            Assert.NotNull(data.Rate);
            Assert.Equal("Saint Pete", data.Rate.City);
            Assert.Equal("33701", data.Rate.Zip);
            Assert.Equal("10.99", data.Rate.State_rate);
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

            _taxProvider.Setup(x => x.GetOrderSalesTax(It.IsAny<GetOrderSalesTaxRequestModel>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act 
            var actualResponse = await _taxController.GetOrderSalesTax(fakeRequest) as OkObjectResult;
            var data = actualResponse.Value as GetOrderSalesTaxResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(data);
            Assert.NotNull(data.SalesTax);
            Assert.Equal(12.90, data.SalesTax.amount_to_collect);
            Assert.Equal(124.88, data.SalesTax.Order_total_amount);
            Assert.NotNull(data.SalesTax.breakdown);
            Assert.Equal(78, data.SalesTax.breakdown.city_taxable_amount);
            Assert.NotNull(data.SalesTax.jurisdictions);
            Assert.Equal( "Saint Pete" , data.SalesTax.jurisdictions.city);
        }
    }
}

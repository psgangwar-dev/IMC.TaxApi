using ICM.TaxApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.TaxApi.Core.Validators
{
    public class TaxProviderValidator : ITaxProviderValidator
    {
        public void ValidateGetTaxRateRequest(GetTaxRateRequest getTaxRateRequest)
        {
            if (getTaxRateRequest == null)
                throw new MissingFieldException("MissingParameter.Request");

            if (getTaxRateRequest.Zip == null)
                throw new MissingFieldException("MissingParameter.Zip");
        }
    }
}

using ICM.TaxApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.TaxApi.Core.Validators
{
    public interface ITaxProviderValidator
    {
        void ValidateGetTaxRateRequest(GetTaxRateRequest getTaxRateRequest);
    }
}

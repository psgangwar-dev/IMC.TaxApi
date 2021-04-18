using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class PartnerConfiguration
    {
        public string ConsumerKey { get; set; }

        public string PartnerBaseEndpoint { get; set; }

        public string AuthToken { get; set; }

        public bool TokenAuthRequired { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class PartnerErrorInfo
    {
        public string status { get; set; }

        public string error { get; set; }

        public string detail { get; set; }
    }
}

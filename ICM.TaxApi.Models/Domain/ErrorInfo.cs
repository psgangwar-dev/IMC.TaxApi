using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class ErrorInfo
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}

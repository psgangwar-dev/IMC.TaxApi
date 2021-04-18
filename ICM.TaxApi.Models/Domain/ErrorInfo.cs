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
        /// Error messages
        /// </summary>
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}

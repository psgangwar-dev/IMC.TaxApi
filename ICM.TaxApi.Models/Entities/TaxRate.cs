using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class TaxRate
    {
        /// <summary>
        /// 
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State_rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string County_rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City_rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Combined_district_rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Combined_rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Freight_taxable { get; set; }
    }
}

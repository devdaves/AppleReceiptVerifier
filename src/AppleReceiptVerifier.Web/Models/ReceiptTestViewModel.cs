using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppleReceiptVerifier.Web.Models
{
    /// <summary>
    /// Receipt Test View Model
    /// </summary>
    public class ReceiptTestViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptTestViewModel"/> class.
        /// </summary>
        public ReceiptTestViewModel()
        {
            this.Environments = new List<SelectListItem>()
                {
                    new SelectListItem { Selected = false, Text = "Apple Production", Value = "production" },
                    new SelectListItem { Selected = false, Text = "Apple Sandbox", Value = "sandbox" },
                };
        }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        [DisplayName("Environment")]
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the environments.
        /// </summary>
        /// <value>
        /// The environments.
        /// </value>
        public List<SelectListItem> Environments { get; set; }

        /// <summary>
        /// Gets or sets the receipt data.
        /// </summary>
        /// <value>
        /// The receipt data.
        /// </value>
        [DisplayName("Receipt Data")]
        public string ReceiptData { get; set; }

        /// <summary>
        /// Gets or sets the receipt response.
        /// </summary>
        /// <value>
        /// The receipt response.
        /// </value>
        public AppleReceiptVerifier.Models.Response ReceiptResponse { get; set; }
    }
}
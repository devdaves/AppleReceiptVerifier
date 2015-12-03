using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppleReceiptVerifier.Models
{
    /// <summary>
    /// Response object
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("Status")]
        public int Status { get; set; }

        public string StatusDescription {
            get
            {
                switch (Status)
                {
                    case 2100:
                        return "The App Store could not read the JSON object you provided.";
                    case 21002:
                        return "The data in the receipt-data property was malformed or missing.";
                    case 21003:
                        return "The receipt could not be authenticated.";
                    case 21004:
                        return "The shared secret you provided does not match the shared secret on file for your account. Only returned for iOS 6 style transaction receipts for auto - renewable subscriptions.";
                    case 21005:
                        return "The receipt server is not currently available.";
                    case 21006:
                        return "This receipt is valid but the subscription has expired. When this status code is returned to your server, the receipt data is also decoded and returned as part of the response. Only returned for iOS 6 style transaction receipts for auto - renewable subscriptions.";
                    case 21007:
                        return "This receipt is from the test environment, but it was sent to the production environment for verification. Send it to the test environment instead.";
                    case 21008:
                        return "This receipt is from the production environment, but it was sent to the test environment for verification. Send it to the production environment instead.";
                    case 1:
                        return "Something went wrong...";
                    case 0:
                        return "OK";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the receipt.
        /// </summary>
        /// <value>
        /// The receipt.
        /// </value>
        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }

        /// <summary>
        /// Gets or sets the latest expired receipt info.
        /// </summary>
        /// <value>
        /// The latest expired receipt info.
        /// </value>
        [JsonProperty("latest_expired_receipt_info")]
        public Receipt LatestExpiredReceiptInfo { get; set; }

        /// <summary>
        /// Gets or sets the raw response.
        /// </summary>
        /// <value>
        /// The raw response.
        /// </value>
        public string RawResponse { get; set; }
    }
}

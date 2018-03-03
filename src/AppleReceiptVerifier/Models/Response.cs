using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Gets or sets the latest receipt info.
        ///  This field is fill only when subscription is still active
        /// </summary>
        /// <value>
        /// The latest receipt info.
        /// </value>
        [JsonProperty("latest_receipt_info")]
        public Receipt LatestReceiptInfo { get; set; }

        /// <summary>
        /// Gets or sets the latest receipt (base 64).
        /// This field is fill only when subscription is still active
        /// </summary>
        /// <value>
        /// The latest receipt (base 64).
        /// </value>
        [JsonProperty("latest_receipt")]
        public string LatestReceipt { get; set; }

        /// <summary>
        /// For an expired subscription, the reason for the subscription expiration.
        /// </summary>
        /// <remarks>
        /// This key is only present for a receipt containing an expired auto-renewable subscription. You can use this value to decide whether to display appropriate messaging in your app for customers to resubscribe.
        /// </remarks>
        /// <see cref="https://developer.apple.com/library/content/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html#//apple_ref/doc/uid/TP40010573-CH106-SW20"/>
        [JsonProperty("expiration_intent")]
        public string ExpirationIntent { get; set; }

        /// <summary>
        /// Descriptive strings for the value found in <see cref="ExpirationIntent"/> 
        /// </summary>
        public string ExpirationIntentDescription
        {
            get
            {
                switch (this.ExpirationIntent)
                {
                    case "1": return "Customer canceled their subscription.";
                    case "2": return "Billing error; for example customer’s payment information was no longer valid.";
                    case "3": return "Customer did not agree to a recent price increase.";
                    case "4": return "Product was not available for purchase at the time of renewal.";
                    case "5": return "Unknown error.";
                    case null:
                    case "":
                        return "No value was set for 'expiration_intent'";
                    default: return "Unknown value";
                }
            }
        }

        /// <summary>
        /// For an expired subscription, whether or not Apple is still attempting to automatically renew the subscription.
        /// </summary>
        /// <remarks>
        /// This key is only present for auto-renewable subscription receipts. If the customer’s subscription failed to renew because the App Store was unable to complete the transaction, this value will reflect whether or not the App Store is still trying to renew the subscription.
        /// </remarks>
        /// <see cref="https://developer.apple.com/library/content/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html#//apple_ref/doc/uid/TP40010573-CH106-SW24"/>
        [JsonProperty("is_in_billing_retry_period")]
        public string IsInBillingRetryPeriod { get; set; }

        /// <summary>
        /// Descriptive strings for the value found in <see cref="IsInBillingRetryPeriod"/> 
        /// </summary>
        public string IsInBillingRetryPeriodDescription
        {
            get
            {
                switch (this.IsInBillingRetryPeriod)
                {
                    case "1": return "App Store is still attempting to renew the subscription.";
                    case "0": return "App Store has stopped attempting to renew the subscription.";
                    case null:
                    case "":
                        return "No value was set for 'is_in_billing_retry_period'";
                    default: return "Unknown value";
                }
            }
        }
             
        
        /// <summary>
        /// Gets or sets the raw response.
        /// </summary>
        /// <value>
        /// The raw response.
        /// </value>
        public string RawResponse { get; set; }
    }
}

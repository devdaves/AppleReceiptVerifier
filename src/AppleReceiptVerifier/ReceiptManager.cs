using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppleReceiptVerifier.Interfaces;
using AppleReceiptVerifier.Models;
using Newtonsoft.Json;

namespace AppleReceiptVerifier
{
    /// <summary>
    /// Receipt Manager
    /// </summary>
    public class ReceiptManager : IReceiptManager
    {
        /// <summary>
        /// The apple HTTP request
        /// </summary>
        private IAppleHttpRequest appleHttpRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptManager" /> class.
        /// </summary>
        public ReceiptManager()
        {
            this.appleHttpRequest = new AppleHttpRequest();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptManager" /> class.
        /// </summary>
        /// <param name="appleHttpRequest">The apple HTTP request.</param>
        internal ReceiptManager(IAppleHttpRequest appleHttpRequest)
        {
            this.appleHttpRequest = appleHttpRequest;
        }

        /// <summary>
        /// Validate Receipt
        /// </summary>
        /// <param name="postUri">Uri to post receipt data to</param>
        /// <param name="receiptData">receipt data from apple</param>
        /// <param name="password">Your app’s shared secret (a hexadecimal string). Only used for receipts that contain auto-renewable subscriptions.</param>
        /// <returns>returns <see cref="Response" />Response</returns>
        public Response ValidateReceipt(Uri postUri, string receiptData, string password = null)
        {
            try
            {
                string receipt64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(receiptData));

                Dictionary<string, string> postObject = new Dictionary<string, string>();
                postObject.Add("receipt-data", receipt64);

                if (!string.IsNullOrEmpty(password))
                {
                    postObject.Add("password", password);
                }
                
                string json = JsonConvert.SerializeObject(postObject);

                var rawResponse = this.appleHttpRequest.GetResponse(postUri, json);
                var serializedResponse = JsonConvert.DeserializeObject<Response>(rawResponse);
                if (serializedResponse != null)
                {
                    serializedResponse.RawResponse = rawResponse;
                    return serializedResponse;
                }
            }
            catch
            {
            }
            
            return new Response() { Status = 1 };
        }
    }
}

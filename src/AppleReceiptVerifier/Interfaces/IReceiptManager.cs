using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppleReceiptVerifier.Models;

namespace AppleReceiptVerifier.Interfaces
{
    /// <summary>
    /// Receipt Manager Interface
    /// </summary>
    public interface IReceiptManager
    {
        /// <summary>
        /// Validate Receipt
        /// </summary>
        /// <param name="postUri">Uri to post receipt data to</param>
        /// <param name="receiptData">receipt data from apple</param>
        /// <param name="password">Your app’s shared secret (a hexadecimal string). Only used for receipts that contain auto-renewable subscriptions.</param>
        /// <returns>returns Response</returns>
        Response ValidateReceipt(Uri postUri, string receiptData, string password = null);
    }
}

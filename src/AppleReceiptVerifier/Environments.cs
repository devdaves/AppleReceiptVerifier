using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleReceiptVerifier
{
    /// <summary>
    /// Environments used to validate a receipt with Apple
    /// </summary>
    public static class Environments
    {
        /// <summary>
        /// Initializes static members of the <see cref="Environments" /> class.
        /// </summary>
        static Environments()
        {
            Sandbox = new Uri("https://sandbox.itunes.apple.com/verifyReceipt");
            Production = new Uri("https://buy.itunes.apple.com/verifyReceipt");
        }

        /// <summary>
        /// Gets the Sandbox URI used for Apple receipt validation
        /// </summary>
        public static Uri Sandbox { get; private set; }

        /// <summary>
        /// Gets the Production URI used for Apple receipt validation 
        /// </summary>
        public static Uri Production { get; private set; }
    }
}

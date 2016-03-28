using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppleReceiptVerifier.Interfaces;
using AppleReceiptVerifier.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace AppleReceiptVerifier.Test
{
    /// <summary>
    /// Receipt Manager Tests
    /// </summary>
    [TestClass]
    public class ReceiptManagerTests
    {
        /// <summary>
        /// Valid response returned from apple via the http request
        /// </summary>
        private string validResponse = JsonConvert.SerializeObject(new
            {
                receipt = new
                    {
                        unique_identifier = "testUniqueIdentifier",
                        original_transaction_id = "testOriginalTransactionId",
                        bvrs = "testApplicationVersionNumber",
                        app_item_id = "testAppItemId",
                        transaction_id = "testTransactionId",
                        quantity = "1",
                        unique_vendor_identifier = "testUniqueVendorIdentifier",
                        product_id = "testProductId",
                        item_id = "testItemId",
                        version_external_identifier = "testVersionExternalIdentifier",
                        bid = "testBundleIdentifier",
                        purchase_date = "2013-01-01 00:00:00 Etc/GMT",
                        purchase_date_ms = "123456789",
                        purchase_date_pst = "2013-01-01 00:00:00 America/Los_Angeles",
                        original_purchase_date = "2013-01-01 00:00:00 Etc/GMT",
                        original_purchase_date_ms = "123456789",
                        original_purchase_date_pst = "2013-01-01 00:00:00 America/Los_Angeles",
                        cancellation_date = "2014-01-01 00:00:00 Etc/GMT",
                },
                status = 0
            });

        /// <summary>
        /// Apple Http Request Mock
        /// </summary>
        private Mock<IAppleHttpRequest> appleHttpRequestMock;

        /// <summary>
        /// Receipt Manager
        /// </summary>
        private ReceiptManager receiptManager;

        /// <summary>
        /// Setup
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.appleHttpRequestMock = new Mock<IAppleHttpRequest>();
            this.receiptManager = new ReceiptManager(this.appleHttpRequestMock.Object);
        }

        /// <summary>
        /// When validating the receipt and the http request returns an exception
        /// the response should return a status of 1
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpRequestReturnsException_Response_StatusShouldBe1()
        {
            this.appleHttpRequestMock.Setup(x => x.GetResponse(It.IsAny<Uri>(), It.IsAny<string>())).Throws(new Exception());
            var response = this.receiptManager.ValidateReceipt(new Uri("http://www.test.com"), string.Empty);
            Assert.AreEqual(1, response.Status);
        }

        /// <summary>
        /// When validating the receipt and the http request returns an empty string
        /// the response should return a status of 1
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpRequestReturnsEmptyResult_Response_StatusShouldBe1()
        {
            this.appleHttpRequestMock.Setup(x => x.GetResponse(It.IsAny<Uri>(), It.IsAny<string>())).Returns(string.Empty);
            var response = this.receiptManager.ValidateReceipt(new Uri("http://www.test.com"), string.Empty);
            Assert.AreEqual(1, response.Status);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a mal formed string
        /// the response should return a status of 1
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpRequestReturnsMalformedResult_Response_StatusShouldBe1()
        {
            this.appleHttpRequestMock.Setup(x => x.GetResponse(It.IsAny<Uri>(), It.IsAny<string>())).Returns("asdjlaksdjlkasjdlkajsdlksjd");
            var response = this.receiptManager.ValidateReceipt(new Uri("http://www.test.com"), string.Empty);
            Assert.AreEqual(1, response.Status);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the
        /// response status should be 0
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_Response_StatusShouldBe0()
        {
            this.ValidateProperty<int>(0, x => x.Status);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the
        /// response raw response should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_Response_RawResponse_ShouldBeValid()
        {
            this.ValidateProperty<string>(this.validResponse, x => x.RawResponse);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the
        /// response receipt unique identifier should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttoResponseValid_ResponseReceipt_UniqueIdentifier_ShouldBeValid()
        {
            this.ValidateProperty<string>("testUniqueIdentifier", x => x.Receipt.UniqueIdentifier);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt unique vendor identifier should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_UniqueVendorIdentifier_ShouldBeValid()
        {
            this.ValidateProperty<string>("testUniqueVendorIdentifier", x => x.Receipt.UniqueVendorIdentifier);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt item id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_ItemId_ShouldBeValid()
        {
            this.ValidateProperty<string>("testItemId", x => x.Receipt.ItemId);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response quantity id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_Quantity_ShouldBeValid()
        {
            this.ValidateProperty<int>(1, x => x.Receipt.Quantity);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt product id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_ProductId_ShouldBeValid()
        {
            this.ValidateProperty<string>("testProductId", x => x.Receipt.ProductId);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt transaction id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_TransactionId_ShouldBeValid()
        {
            this.ValidateProperty<string>("testTransactionId", x => x.Receipt.TransactionId);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt original transaction id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_OriginalTransactionId_ShouldBeValid()
        {
            this.ValidateProperty<string>("testOriginalTransactionId", x => x.Receipt.OriginalTransactionId);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt app item id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_AppItemId_ShouldBeValid()
        {
            this.ValidateProperty<string>("testAppItemId", x => x.Receipt.AppItemId);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt version external identifier id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_VersionExternalIdentifier_ShouldBeValid()
        {
            this.ValidateProperty<string>("testVersionExternalIdentifier", x => x.Receipt.VersionExternalIdentifier);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt bundle identifier id should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_BundleIdentifier_ShouldBeValid()
        {
            this.ValidateProperty<string>("testBundleIdentifier", x => x.Receipt.BundleIdentifier);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt application version number should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_ApplicationVersionNumber_ShouldBeValid()
        {
            this.ValidateProperty<string>("testApplicationVersionNumber", x => x.Receipt.ApplicationVersionNumber);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt purchase date UTC should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_PurchaseDateUtc_ShouldBeValid()
        {
            this.ValidateProperty<DateTime>(new DateTime(2013, 1, 1), x => x.Receipt.PurchaseDateUtc);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt purchase date PST should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_PurchaseDatePst_ShouldBeValid()
        {
            this.ValidateProperty<DateTime>(new DateTime(2013, 1, 1), x => x.Receipt.PurchaseDatePst);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt purchase date milliseconds should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_PurchaseDateMilliseconds_ShouldBeValid()
        {
            this.ValidateProperty<long>(123456789, x => x.Receipt.PurchaseDateMilliseconds);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt cancellation date UTC should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_CancellationDateUtc_ShouldBeValid()
        {
            this.ValidateProperty<DateTime>(new DateTime(2014, 1, 1), x => x.Receipt.CancellationDateUtc);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt original purchase date UTC should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_OriginalPurchaseDateUtc_ShouldBeValid()
        {
            this.ValidateProperty<DateTime>(new DateTime(2013, 1, 1), x => x.Receipt.OriginalPurchaseDateUtc);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt original purchase date PST should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_OriginalPurchaseDatePst_ShouldBeValid()
        {
            this.ValidateProperty<DateTime>(new DateTime(2013, 1, 1), x => x.Receipt.OriginalPurchaseDatePst);
        }

        /// <summary>
        /// When validating the receipt and the http request returns a valid response the 
        /// response receipt original purchase date milliseconds should be valid
        /// </summary>
        [TestMethod]
        public void ValidateReceipt_HttpResponseValid_ResponseReceipt_OriginalPurchaseDateMilliseconds_ShouldBeValid()
        {
            this.ValidateProperty<long>(123456789, x => x.Receipt.PurchaseDateMilliseconds);
        }

        /// <summary>
        /// Generic method to validate the property of the Response
        /// </summary>
        /// <typeparam name="TType">Data Type to compare</typeparam>
        /// <param name="expected">Expected value</param>
        /// <param name="actualFunc">Actual FUNC to invoke</param>
        private void ValidateProperty<TType>(TType expected, Func<Response, TType> actualFunc)
        {
            this.appleHttpRequestMock.Setup(x => x.GetResponse(It.IsAny<Uri>(), It.IsAny<string>())).Returns(this.validResponse);
            var response = this.receiptManager.ValidateReceipt(new Uri("http://www.test.com"), string.Empty);
            Assert.AreEqual(expected, actualFunc.Invoke(response));
        }
    }
}

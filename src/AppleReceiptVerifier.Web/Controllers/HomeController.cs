using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppleReceiptVerifier.Web.Models;
using Newtonsoft.Json;

namespace AppleReceiptVerifier.Web.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>returns ActionResult</returns>
        public ActionResult Index()
        {
            ReceiptTestViewModel model = new ReceiptTestViewModel();
            model.Environment = "production";
            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns ActionResult</returns>
        [HttpPost]
        public ActionResult Index(ReceiptTestViewModel model)
        {
            ReceiptManager receiptManager = new ReceiptManager();
            var env = AppleReceiptVerifier.Environments.Production;
            if (model.Environment == "sandbox")
            {
                env = AppleReceiptVerifier.Environments.Sandbox;
            }

            var response = receiptManager.ValidateReceipt(env, model.ReceiptData);
            model.ReceiptResponse = response;

            return this.View(model);
        }
    }
}

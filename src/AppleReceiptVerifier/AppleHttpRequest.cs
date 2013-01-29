using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppleReceiptVerifier.Interfaces;

namespace AppleReceiptVerifier
{
    /// <summary>
    /// Apple Http Request
    /// </summary>
    internal class AppleHttpRequest : IAppleHttpRequest
    {
        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The data to be posted.</param>
        /// <returns>
        /// response as string
        /// </returns>
        public string GetResponse(Uri url, string postData)
        {
            string response = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.ContentType = "text/plain";
                webRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                WebResponse webResponse = webRequest.GetResponse();
                using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch
            {
            }

            return response;
        }
    }
}

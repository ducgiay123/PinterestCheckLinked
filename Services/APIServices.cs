using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeViaFBTool.Models;
using Leaf.xNet;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading;
using System.Security.Policy;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net;

namespace ChangeViaFBTool.Services
{
    public class APIServices
    {
        private string _lastID = string.Empty;
        

        public APIOptions.APIEmailResult GetCodeService(APIOptions.VerifyEmailOptions options)
        {
            var result = new APIOptions.APIEmailResult();
            HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
            httpRequest.ConnectTimeout = 10000;
            //httpRequest.ReadWriteTimeout = 5;

            HttpResponse httpResponse = null;
            try
            {
                AddCustomHeaders(httpRequest);
                string example = "VXWRQ4RPZDJZ2IDI3X64NVDV5RUQPNIH";
                string url2faLive = $"https://2fa.live/tok/{example}";
                httpResponse = httpRequest.Get(url2faLive);
                ApiResponse data = JsonConvert.DeserializeObject<ApiResponse>(httpResponse.ToString());
                httpResponse = null ;
                result.TwoFACode = data.Token;  

            }
            catch (Exception ex)
            {

            }
            finally
            {
                httpRequest?.Close();
                httpRequest?.Dispose();
                httpResponse = null;
            }
            return result;
        }



        private void AddCustomHeaders(HttpRequest httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            httpRequest["accept"] = "*/*";
            httpRequest["referer"] = "https://2fa.live/";
            httpRequest["x-requested-with"] = "XMLHttpRequest";
            httpRequest["user-agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36";
            httpRequest["sec-ch-ua"] = "\"Chromium\";v=\"134\", \"Not:A-Brand\";v=\"24\", \"Google Chrome\";v=\"134\"";
            httpRequest["sec-ch-ua-mobile"] = "?0";
            httpRequest["sec-ch-ua-platform"] = "\"Windows\"";
        }


        static void EditPayloadValues(RequestParams payload, string newEmail)
        {
            // Update the values in the payload
            payload["email"] = newEmail;
            //payload["prefill_contact_point"] = newEmail;
        }
        public static void SetUpCookiesHeaderForAPIRequest(HttpRequest httpRequest, string headers)
        {
            var headerDictionary = new Dictionary<string, string>();

            foreach (var line in headers.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var separatorIndex = line.IndexOf(':');
                if (separatorIndex > -1)
                {
                    string key = line.Substring(0, separatorIndex).Trim();
                    string value = line.Substring(separatorIndex + 1).Trim();
                    headerDictionary[key] = value;
                }
            }
            foreach (var item in headerDictionary)
            {
                httpRequest.AddHeader(item.Key, item.Value);
            }
        }
        public static RequestParams ParseStringToPayload(string rawString)
        {
            var payload = new RequestParams();
            var lines = rawString.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    payload[key] = value;
                }
            }

            return payload;
        }
    }
}

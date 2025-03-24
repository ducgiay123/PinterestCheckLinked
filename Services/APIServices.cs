using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckDieLinkedinToolV3.Models;
using Leaf.xNet;
using System.Text.Json;
using System.Threading;
using System.Security.Policy;

namespace CheckDieLinkedinToolV3.Services
{
    public class APIServices
    {
        private string header = "accept:*/*\r\naccept-language:en-GB,en-US;q=0.9,en;q=0.8\r\ncontent-type:application/x-www-form-urlencoded\r\ncookie:datr=J7dlZy5czTFzv2W_-ljG1Ryq; sb=TbdlZyw6xpBYCt1kym953DgX; dpr=1.5; ps_l=1; ps_n=1; av2=1t1734809157; wd=899x932\r\norigin:https://www.facebook.com\r\npriority:u=1, i\r\nreferer:https://www.facebook.com/login/identify/?ctx=recover&next=https%3A%2F%2Fwww.facebook.com%2F&from_login_screen=0\r\nsec-ch-ua:\"Google Chrome\";v=\"131\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"\r\nsec-ch-ua-mobile:?0\r\nsec-ch-ua-platform:\"Windows\"\r\nsec-fetch-dest:empty\r\nsec-fetch-mode:cors\r\nsec-fetch-site:same-origin\r\nuser-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36\r\nx-asbd-id:129477\r\nx-fb-lsd:AVoNYbM4Rqc";
        private  string urlVerify = "https://www.facebook.com/ajax/login/help/identify.php?next=https%3A%2F%2Fwww.facebook.com%2F&ctx=recover";
        string searchString = "#account_search";
        //public LinkedinAPIExecuteResult.VerifyEmailResult APICheckLinkedToPinterest(LinkedinAPIOptions.VerifyEmailOptions options) {
        //    var result = new LinkedinAPIExecuteResult.VerifyEmailResult();
        //    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Success;
        //    result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
        //    HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
        //    httpRequest.ConnectTimeout = 5; // Timeout for connection (milliseconds)
        //    httpRequest.ReadWriteTimeout = 5;
        //    HttpResponse httpResponse = null;
        //    try
        //    {
        //        //string[] email = options.Email.Split('@');
        //        string payloadString = $"jazoest:2950\r\nlsd:AVoNYbM4Rqc\r\nemail:{options.Email}\r\ndid_submit:1\r\n__user:0\r\n__a:1\r\n__req:5\r\n__hs:20078.BP:DEFAULT.2.0.0.0.0\r\ndpr:1\r\n__ccg:MODERATE\r\n__rev:1019053202\r\n__s:g93pfa:w5h4rc:f3zec4\r\n__hsi:7450950827889870537\r\n__dyn:7xeUmwkHg7ebwKBAg5S1Dxu13wqovzEdEc8uxa0CEbo1nEhwem0nCq1ewcG0RU2Cw8G1Qw5Mx61vw4iwBgao6C0Mo2swaO4U2zxe3C0D888co5G0zE5W0HU1uUdEGdw46wbS1Lwqo15E6O0ue1TwmU3yw\r\n__csr:\r\n__spin_r:1019053202\r\n__spin_b:trunk\r\n__spin_t:1734809677";
                
        //        var payload = ParseStringToPayload(payloadString);
        //        SetUpCookiesHeaderForAPIRequest(httpRequest , header);
        //        //httpRequest.UserAgent = Http.RandomUserAgent();
        //        httpResponse = httpRequest.Post(urlVerify, payload);

        //        if (httpResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            string responseString = httpResponse.ToString();

        //            if (responseString.Contains(searchString))
        //            {
        //                result.EmailStatusCode = Enums.VerifyEmailStatusCode.NotLinked;
        //            }
        //            else if(responseString.Contains("ldata")) {
        //                result.EmailStatusCode = Enums.VerifyEmailStatusCode.Linked;
        //            }
        //            else
        //            {
        //                result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //            }
        //        }
        //        else
        //        {
        //            result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Exception = ex;
        //        result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //    }
        //    finally
        //    {
        //        httpRequest?.Close();
        //        httpRequest?.Dispose();
        //        httpResponse = null;
        //    }
        //    return result;
        //}
        //public LinkedinAPIExecuteResult.VerifyEmailResult RecoveryAPIExecution(LinkedinAPIOptions.VerifyEmailOptions options)
        //{
        //    var result = new LinkedinAPIExecuteResult.VerifyEmailResult();
        //    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Success;
        //    result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
        //    HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
        //    httpRequest.ConnectTimeout = 5; // Timeout for connection (milliseconds)
        //    httpRequest.ReadWriteTimeout = 5;
        //    httpRequest.Cookies.Clear();
        //    httpRequest.Cookies =  new CookieStorage();
        //    HttpResponse httpResponse = null;
        //    HttpRequest httpRequestSendCode = HttpFactory.NewClient(options.HttpConfig);
        //    httpRequestSendCode.Cookies = new CookieStorage();
        //    httpRequestSendCode.Cookies.Clear();
        //    try
        //    {
        //        string payloadString = $"jazoest:2950\r\nlsd:AVoNYbM4Rqc\r\nemail:{options.Email}\r\ndid_submit:1\r\n__user:0\r\n__a:1\r\n__req:5\r\n__hs:20078.BP:DEFAULT.2.0.0.0.0\r\ndpr:1\r\n__ccg:MODERATE\r\n__rev:1019053202\r\n__s:g93pfa:w5h4rc:f3zec4\r\n__hsi:7450950827889870537\r\n__dyn:7xeUmwkHg7ebwKBAg5S1Dxu13wqovzEdEc8uxa0CEbo1nEhwem0nCq1ewcG0RU2Cw8G1Qw5Mx61vw4iwBgao6C0Mo2swaO4U2zxe3C0D888co5G0zE5W0HU1uUdEGdw46wbS1Lwqo15E6O0ue1TwmU3yw\r\n__csr:\r\n__spin_r:1019053202\r\n__spin_b:trunk\r\n__spin_t:1734809677";

        //        var payload = ParseStringToPayload(payloadString);
        //        SetUpCookiesHeaderForAPIRequest(httpRequest, header);
        //        //httpRequest.UserAgent = Http.RandomUserAgent();
        //        httpResponse = httpRequest.Post(urlVerify, payload);
        //        Task.Delay(TimeSpan.FromSeconds(2)).Wait();
        //        if (httpResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            try
        //            {
        //                var cookies = httpRequest.Cookies.GetCookies(urlVerify);
        //                var cookie = cookies?["sfiu"]; // Use null-conditional operator
        //                string stringCookie = cookie?.ToString().Replace("sfiu=", "") ?? string.Empty; // Handle null case
        //                httpRequest.Cookies.Clear();
        //                string payloadSendCodeString = "jazoest:2885\r\nlsd:AVrBOh1UJ8k\r\n__aaid:0\r\n__user:0\r\n__a:1\r\n__req:6\r\n__hs:20088.BP:DEFAULT.2.0.0.0.0\r\ndpr:1\r\n__ccg:GOOD\r\n__rev:1019106461\r\n__s:q3rwec:hcj59i:tpr9k0\r\n__csr:\r\n__spin_r:1019106461\r\n__spin_b:trunk\r\n__spin_t:1735656467";
        //                var payLoadSendCode = ParseStringToPayload(payloadSendCodeString);
        //                string urlSendCode = $"https://www.facebook.com/ajax/recover/initiate/?recover_method=send_email&selected_cuid={stringCookie}";
        //                AddCustomHeaders(httpRequestSendCode);
        //                httpResponse = null;
        //                //SetUpCookiesHeaderForAPIRequest(httpRequestSendCode, header);
        //                httpResponse = httpRequestSendCode.Post(urlSendCode, payLoadSendCode);
        //                if (httpResponse.StatusCode == HttpStatusCode.OK)
        //                {
        //                    string responseString = httpResponse.ToString();
        //                    if (responseString.Contains("send_email"))
        //                    {
        //                        result.EmailStatusCode = Enums.VerifyEmailStatusCode.Linked;
        //                    }
        //                    else
        //                    {
        //                        result.EmailStatusCode = Enums.VerifyEmailStatusCode.NotLinked;
        //                    }
        //                }
        //                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
        //            }
        //            catch {
        //                result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //            }
        //        }
        //        else
        //        {
        //            result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Exception = ex;
        //        result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
        //    }
        //    finally
        //    {
        //        httpRequest?.Close();
        //        httpRequest?.Dispose();
        //        httpResponse = null;
        //    }
        //    return result;
        //}
        private void AddCustomHeaders(HttpRequest httpRequest)
        {
            httpRequest.AddHeader("accept", "*/*");
            httpRequest.AddHeader("accept-language", "en-GB,en-US;q=0.9,en;q=0.8");
            httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            httpRequest.AddHeader("origin", "https://www.facebook.com");
            httpRequest.AddHeader("priority", "u=1, i");
            httpRequest.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"131\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"");
            httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
            httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            httpRequest.AddHeader("sec-fetch-dest", "empty");
            httpRequest.AddHeader("sec-fetch-mode", "cors");
            httpRequest.AddHeader("sec-fetch-site", "same-origin");
            httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            httpRequest.AddHeader("x-asbd-id", "129477");
            httpRequest.AddHeader("x-fb-lsd", "AVrBOh1UJ8k");
        }
        private void AddHeadersToRequest(HttpRequest httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException(nameof(httpRequest));

            // Add headers using AddHeader method
            httpRequest.AddHeader("accept", "*/*");
            httpRequest.AddHeader("accept-language", "en-GB,en-US;q=0.9,en;q=0.8");
            httpRequest.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
            httpRequest.AddHeader("cookie", "A1=d=AQABBDXTZWcCEHhX5iSkA-mvKu9Qn1fLulwFEgEBAQEkZ2dvZ8Cd0CMA_eMAAA&S=AQAAAu41FiMNJn1JC_je2NYDu_s; A3=d=AQABBDXTZWcCEHhX5iSkA-mvKu9Qn1fLulwFEgEBAQEkZ2dvZ8Cd0CMA_eMAAA&S=AQAAAu41FiMNJn1JC_je2NYDu_s; A1S=d=AQABBDXTZWcCEHhX5iSkA-mvKu9Qn1fLulwFEgEBAQEkZ2dvZ8Cd0CMA_eMAAA&S=AQAAAu41FiMNJn1JC_je2NYDu_s; cmp=t=1734726455&j=0&u=1---; gpp=DBAA; gpp_sid=-1; axids=gam=y-PX.YLotE2uJ_khM9cysjvempMgygnXOD~A&dv360=eS1IbnRDRjdaRTJ1R3dUME1TZG5xMjJDS1JVZHJZcUxVbH5B&ydsp=y-T6XY5rxE2uJrSJpo6XuIl1Oo15qED4sI~A&tbla=y-aPa40.dE2uJZZeIkltX13N3EI8IdxKOg~A&tbla_id=5a0a7c92-688c-43d2-82ea-1a99705019ac-tucte5f58b7; AS=v=1&s=e7muPgDM...");
            httpRequest.AddHeader("origin", "https://login.yahoo.com");
            httpRequest.AddHeader("priority", "u=1, i");
            httpRequest.AddHeader("referer", "https://login.yahoo.com/account/create?.intl=gb&.lang=en-GB&src=ym&activity=mail-direct&pspid=159600001&.done=https%3A%2F%2Fmail.yahoo.com%2Fd%2F&specId=yidregsimplified&done=https%3A%2F%2Fmail.yahoo.com%2Fd%2F");
            httpRequest.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"131\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"");
            httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
            httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            httpRequest.AddHeader("sec-fetch-dest", "empty");
            httpRequest.AddHeader("sec-fetch-mode", "cors");
            httpRequest.AddHeader("sec-fetch-site", "same-origin");
            httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            httpRequest.AddHeader("x-requested-with", "XMLHttpRequest");
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

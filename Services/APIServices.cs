using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinterestCheckLinked.Models;
using Leaf.xNet;
using System.Text.Json;

namespace PinterestCheckLinked.Services
{
    public class APIServices
    {
        public PinterestAPIExecuteResult.VerifyEmailResult APICheckLinkedToPinterest(PinterestAPIOptions.VerifyEmailOptions options) {
            var result = new PinterestAPIExecuteResult.VerifyEmailResult();
            result.StatusCode = Enums.PinterestAPIExecuteStatusCode.Success;
            result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
            HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
            httpRequest.UserAgent = Http.RandomUserAgent();
            HttpResponse httpResponse = null;
            try
            {
                string[] email = options.Email.Split('@');
                //string testProxy = $"https://api.ipify.org/?format=json";
                string urlVerify = $"https://fi.pinterest.com/resource/ApiResource/get/?source_url=%2Fpassword%2Freset%2F&data=%7B%22options%22%3A%7B%22url%22%3A%22%2Fv3%2Fregister%2Fexists%2F%22%2C%22data%22%3A%7B%22email%22%3A%22{email[0]}%40{email[1]}%22%7D%7D%2C%22context%22%3A%7B%7D%7D&_=1731427608635";
                //httpResponse = httpRequest.Get(testProxy);
                httpResponse = httpRequest.Get(urlVerify);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    string jsonContent = httpResponse.ToString();
                    using (JsonDocument jsonDoc = JsonDocument.Parse(jsonContent))
                    {
                        JsonElement root = jsonDoc.RootElement;

                        if (root.TryGetProperty("resource_response", out JsonElement resourceResponse))
                        {
                            if (resourceResponse.TryGetProperty("data", out JsonElement dataElement))
                            {
                                bool isLinked = dataElement.GetBoolean();
                                result.EmailStatusCode = isLinked ? Enums.VerifyEmailStatusCode.Linked : 
                                                                    Enums.VerifyEmailStatusCode.NotLinked;

                            }
                            else
                            {
                                result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
                            }
                           
                        }
                        else
                        {
                            result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
                        }
                    }
                }
                else if (httpResponse.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
                    result.StatusCode = Enums.PinterestAPIExecuteStatusCode.Error;
                }
                else
                {
                    result.StatusCode = Enums.PinterestAPIExecuteStatusCode.Error;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.StatusCode = Enums.PinterestAPIExecuteStatusCode.Error;
            }
            finally
            {
                httpRequest?.Close();
                httpRequest?.Dispose();
                httpResponse = null;
            }
            return result;
        }
    }
}

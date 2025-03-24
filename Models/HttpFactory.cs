using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeViaFBTool.Models;
using Leaf.xNet;

namespace ChangeViaFBTool.Models
{
    public class HttpFactory
    {
        public static HttpRequest NewClient(HttpConfig config, bool useSSL = true)
        {
            HttpRequest httpRequest = new HttpRequest();
            httpRequest.KeepAlive = true;
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = true;
            if (useSSL)
            {
                httpRequest.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            }
            httpRequest.Cookies = new CookieStorage(false);
            if(config.UseProxy && config.Proxy != null)
            {
                httpRequest.Proxy = config.Proxy;
                httpRequest.Proxy.ReadWriteTimeout = config.ConnectTimeOut;
                httpRequest.Proxy.ConnectTimeout = config.ConnectTimeOut;
            }
            if (!string.IsNullOrEmpty(config.CustomUserAgent))
            {
                httpRequest.UserAgent = config.CustomUserAgent;
            }
            return httpRequest;
        }
    }
}

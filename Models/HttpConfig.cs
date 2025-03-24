using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;

namespace CheckDieLinkedinToolV3.Models
{
    public class HttpConfig
    {
        public ProxyClient Proxy { get; set; }
        public bool UseProxy { get; set; }
        public string CustomUserAgent { get; set; }
        public int ConnectTimeOut { get; set; }
        public static HttpConfig Default
        {
            get
            {
                return new HttpConfig
                {
                    UseProxy = false
                };
            }
        }
    }
}

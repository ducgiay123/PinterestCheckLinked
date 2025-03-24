using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChangeViaFBTool.Models
{
    public class APIOptions
    {
        public class APIEmailResult 
        {
            //public string EmailName = string.Empty;
            //public string FirstCode = string.Empty;
            //public bool IsReceive = false;
            public string TwoFACode= string.Empty;
        }
        public class CheckpointEmailOptions : BaseAPIOptions
        {

        }
        public class VerifyEmailOptions : BaseAPIOptions
        {

        }
        public class BaseAPIOptions
        {
            public string twoFaCode  { get; set; }
            //public string Email { get; set; }
            public HttpConfig HttpConfig { get; set; }
        }
    }
    public class ApiResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    // Class for each email item

}

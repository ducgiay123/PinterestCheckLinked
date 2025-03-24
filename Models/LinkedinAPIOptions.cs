using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckDieLinkedinToolV3.Models
{
    public class LinkedinAPIOptions
    {
        public class CheckpointEmailOptions : BaseAPIOptions
        {

        }
        public class VerifyEmailOptions : BaseAPIOptions
        {

        }
        public class BaseAPIOptions
        {
            public string Email { get; set; }
            public HttpConfig HttpConfig { get; set; }
        }
    }
}

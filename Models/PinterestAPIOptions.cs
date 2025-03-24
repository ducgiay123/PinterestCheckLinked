using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinterestCheckLinked.Models
{
    public class PinterestAPIOptions
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

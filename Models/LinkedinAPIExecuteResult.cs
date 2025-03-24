using CheckDieLinkedinToolV3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckDieLinkedinToolV3.Models
{
    public class LinkedinAPIExecuteResult
    {
        public class CheckpointEmailSubmitResult : BaseAPIExecuteResult
        {
            public CheckpointEmailStatusCode CheckpointEmailStatus { get; set; }
        }
        public class VerifyEmailResult : BaseAPIExecuteResult
        {
            public VerifyEmailStatusCode EmailStatusCode { get; set; }
        }
        public class BaseAPIExecuteResult
        {
            public Exception Exception { get; set; }
            public LinkedinAPIExecuteStatusCode StatusCode { get; set; }
        }
    }
}

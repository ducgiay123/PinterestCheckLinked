using ChangeViaFBTool.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeViaFBTool.Models
{
    public class FacecbookAPIExecuteResult
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
            public FacebookAPIExecuteStatusCode StatusCode { get; set; }
        }
    }
}

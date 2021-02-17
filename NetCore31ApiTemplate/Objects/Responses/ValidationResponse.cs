using System.Collections.Generic;

namespace NetCore31ApiTemplate.Objects.Responses
{
    public class ValidationResponse : BaseResponse
    {
        public Dictionary<string, string> ValidationErrors { get; set; }
    }
}
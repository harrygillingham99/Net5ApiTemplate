using System.Collections.Generic;

namespace NetCore5ApiTemplate.Objects.Responses
{
    public class ValidationResponse : BaseResponse
    {
        public Dictionary<string, string> ValidationErrors { get; set; }
    }
}
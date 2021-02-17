using System.Collections.Generic;

namespace NetCore31ApiTemplate.Objects.Responses
{
    public class NotFoundResponse : BaseResponse
    {
        public Dictionary<string, string> BadProperties { get; set; }
    }
}
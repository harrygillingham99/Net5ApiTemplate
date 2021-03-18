namespace NetCore5ApiTemplate.Objects.Responses
{
    public class RequestAudit
    {
        public RequestAudit(ClientMetadata metadata, long elapsedMilliseconds, string requestedEndpoint)
        {
            Metadata = metadata;
            ElapsedMilliseconds = elapsedMilliseconds;
            RequestedEndpoint = requestedEndpoint;
        }

        public ClientMetadata Metadata { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public string RequestedEndpoint { get; set; }
    }
}
using NetCore5ApiTemplate.Attributes;
using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NetCore5ApiTemplate.NSwag
{
    [ScrutorIgnore]
    public class HeaderParameterOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            context.OperationDescription.Operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "Metadata",
                    Kind = OpenApiParameterKind.Header,
                    Type = JsonObjectType.Object,
                    IsRequired = false,
                    Description = "Client Metadata",
                    Default = null
                });

            return true;
        }
    }
}
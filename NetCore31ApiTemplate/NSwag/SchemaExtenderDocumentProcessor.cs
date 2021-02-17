using System;
using System.Linq;
using System.Reflection;
using NetCore31ApiTemplate.Attributes;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NetCore31ApiTemplate.NSwag
{
    [ScrutorIgnore]
    public class SchemaExtenderDocumentProcessor : IDocumentProcessor
    {
        public void Process(DocumentProcessorContext context)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(NSwagIncludeAttribute)).Any())
                    {
                        if (!context.SchemaResolver.HasSchema(type, type.IsEnum))
                        {
                            context.SchemaGenerator.Generate(type, context.SchemaResolver);
                        }
                    }
                }
            }
        }
    }
}
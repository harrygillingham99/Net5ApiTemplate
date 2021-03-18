using System;
using System.Linq;
using System.Reflection;
using NetCore5ApiTemplate.Attributes;
using NetCore5ApiTemplate.Objects;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NetCore5ApiTemplate.NSwag
{
    [ScrutorIgnore]
    public class SchemaExtenderDocumentProcessor : IDocumentProcessor
    {
        private const string NamespaceIdentifier = "RepoAnalyser.";
        private readonly Type[] _typesToLoadAssembliesOf = { typeof(AppSettings) };

        public void Process(DocumentProcessorContext context)
        {
            //Only load specific assemblies
            var assemblies = _typesToLoadAssembliesOf.Select(x => x.GetTypeInfo().Assembly);

            //Merge the lists of assemblies and check for any types with the [NSwagInclude] attribute
            var types = assemblies.SelectMany(x => x.ExportedTypes).Where(type =>
                type.FullName != null && type.FullName.StartsWith(NamespaceIdentifier) &&
                type.GetTypeInfo().CustomAttributes.Any(x => x.AttributeType == typeof(NSwagIncludeAttribute)));

            //Add the types to the schema
            foreach (var type in types)
                if (!context.SchemaResolver.HasSchema(type, type.IsEnum))
                    context.SchemaGenerator.Generate(type, context.SchemaResolver);
        }
    }
}
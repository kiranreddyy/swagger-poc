namespace Swagger.POC.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.Swagger;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Description;

    public class ExamplesOperationFilter : IOperationFilter
    {
        public void Apply(
            Operation operation,
            SchemaRegistry schemaRegistry,
            ApiDescription apiDescription)
        {
            //SetRequestModelExamples(operation, schemaRegistry, apiDescription);
            //SetResponseModelExamples(operation, schemaRegistry, apiDescription);

            var responseAttributes = apiDescription
                .GetControllerAndActionAttributes<SwaggerResponseExamplesAttribute>();

            foreach (var attr in responseAttributes)
            {
                var schema = schemaRegistry.GetOrRegister(attr.ResponseType);

                var response = operation.responses.FirstOrDefault
                    (x => x.Value.schema.type == schema.type
                    && x.Value.schema.@ref == schema.@ref).Value;

                if (response != null)
                {
                    var provider = (IProvideExamples)Activator.CreateInstance(attr.ExamplesType);
                    response.examples = FormatAsJson(provider);
                }
            }
        }

        private static object FormatAsJson(IProvideExamples provider)
        {
            var examples = new Dictionary<string, object>()
        {
            {
                "application/json", provider.GetExamples()
            }
        };

            return ConvertToCamelCase(examples);
        }

        private static object ConvertToCamelCase(Dictionary<string, object> examples)
        {
            var jsonString = JsonConvert.SerializeObject(examples, 
                new JsonSerializerSettings() {
                    ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}
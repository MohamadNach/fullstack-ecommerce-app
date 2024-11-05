using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ecommerce.Infrastructure.src
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Type = "string";
                schema.Enum = Enum.GetValues(context.Type)
                                   .Cast<Enum>()
                                   .Select(e => new OpenApiString(e.ToString()))
                                   .Cast<IOpenApiAny>() // Ensure the cast is to IOpenApiAny
                                   .ToList();
            }
        }
    }
}

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class HideUserIdPropertyFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var createAssetRequestModel = swaggerDoc.Components.Schemas
            .FirstOrDefault(s => s.Key == "CreateAssetRequest").Value;

        if (createAssetRequestModel == null) return;

        if (createAssetRequestModel.Properties.ContainsKey("UserId"))
        {
            createAssetRequestModel.Properties.Remove("UserId");
        }
    }
}

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infeco.Api.Common.SwashBuckle
{
    /// <summary>
    /// AttachOperationNameFilter
    /// </summary>
    public class AttachOperationNameFilter : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation">operation</param>
        /// <param name="context">context</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Summary = $"{operation.Summary} ({operation.OperationId})";
        }
    }
}

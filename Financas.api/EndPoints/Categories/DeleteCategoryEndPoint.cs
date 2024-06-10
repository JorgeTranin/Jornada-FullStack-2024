using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;

namespace Financas.api.EndPoints.Categories
{
    public class DeleteCategoryEndPoint : IEndPoint
    {
           public static void Map(IEndpointRouteBuilder app)

            => app.MapPost("/{id}", HadleAsync)
            .WithName("categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Category>>();
        
        private static async Task<IResult> HadleAsync(ICategoryHandler handler, long id)
        {

            var request = new DeleteCategoryRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteCategoryAsync(request);
            if (result.IsSuccess)
            {
                return TypedResults.Created($"v1/categories/{result.Data?.Id}", result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
    }
}
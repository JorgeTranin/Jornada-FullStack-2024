using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;

namespace Financas.api.EndPoints.Categories
{
    public class CreateCategoryEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)

            => app.MapPost("/", HadleAsync)
            .WithName("categories: Create")
            .WithSummary("Criar uma nova categoria")
            .WithDescription("Criar uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category>>();
        
        private static async Task<IResult> HadleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateCategoryAsync(request);
            if (response.IsSuccess)
            {
                return TypedResults.Created($"v1/categories/{response.Data?.Id}", response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
    }
}
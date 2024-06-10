using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Financas.api.EndPoints.Categories
{
    public class GetCategoryByIdEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HadleAsync)
            .WithName("Categories: Get")
            .WithSummary("Busca categoria")
            .WithDescription("Busca categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HadleAsync(
        ICategoryHandler handler,
        long id)
        {

            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                Id = id,
            };

            var result = await handler.GetCategoryByIdAsync(request);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
    }
}
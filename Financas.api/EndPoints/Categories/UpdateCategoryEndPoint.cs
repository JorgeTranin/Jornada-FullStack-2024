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
    public class UpdateCategoryEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HadleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();
        private static async Task<IResult> HadleAsync(
       ICategoryHandler handler,
       long id,
       UpdateCategoryRequest request)
        {

            request.UserId = ApiConfiguration.UserId ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateCategoryAsync(request);
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
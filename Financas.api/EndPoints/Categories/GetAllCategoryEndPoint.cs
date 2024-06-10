using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Financas.api.EndPoints.Categories
{
    public class GetAllCategoryEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/", HadleAsync)
         .WithName("Categories: Get All")
         .WithSummary("Busca todas as categoria")
            .WithDescription("Busca todas as categoria")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();


        private static async Task<IResult> HadleAsync(
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
        {

            var request = new GetAllCategoryRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllCategoryAsync(request);
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
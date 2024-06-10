using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Financas.api.EndPoints.Categories
{
    public class GetTransactionByPeriodEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HadleAsync)
         .WithName("transactions: Get All")
         .WithSummary("Busca todas as transações")
            .WithDescription("Busca todas as transações")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();
    private static async Task<IResult> HadleAsync(
        ITransactionHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int  pagedNumber = Configuration.DefaultPageNumber,
        [FromQuery] int  pagedSize = Configuration.DefaultPageSize
        )
        {

            var request = new GetTransactionByPeriodRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pagedNumber,
                PageSize = pagedSize
            };

            var result = await handler.GetTransactionByPeriodAsync(request);
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
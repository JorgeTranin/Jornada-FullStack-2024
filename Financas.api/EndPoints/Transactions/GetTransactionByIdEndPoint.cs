using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Financas.api.EndPoints.Categories
{
    public class GetTransactionByIdEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HadleAsync)
         .WithName("transactions: Get All")
         .WithSummary("Busca todas as transações")
            .WithDescription("Busca todas as transações")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();
    private static async Task<IResult> HadleAsync(
        ITransactionHandler handler,
        long id
        )
        {

            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                Id = id
            };

            var result = await handler.GetTransactionByIdAsync(request);
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
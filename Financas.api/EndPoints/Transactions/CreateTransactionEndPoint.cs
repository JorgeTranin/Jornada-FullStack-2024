using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.api.Common.Api;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;

namespace Financas.api.EndPoints.Categories
{
    public class CreateTransactionEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HadleAsync)
            .WithName("categories: Create")
            .WithSummary("Criar uma nova categoria")
            .WithDescription("Criar uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Transaction>>();

        private static async Task<IResult> HadleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateTransactionAsync(request);
            if (response.IsSuccess)
            {
                return TypedResults.Created($"/{response.Data?.Id}", response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }

    }
}
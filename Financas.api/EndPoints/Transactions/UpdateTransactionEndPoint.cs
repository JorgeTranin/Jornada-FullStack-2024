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
    public class UpdateTransactionEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/", HadleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma Transação")
            .WithDescription("Atualiza uma Transação")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();
        private static async Task<IResult> HadleAsync(
       ITransactionHandler handler,
       long id,
       UpdateTransactionRequest request)
        {

            request.UserId = ApiConfiguration.UserId ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateTransactionAsync(request);
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
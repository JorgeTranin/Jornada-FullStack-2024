using Financas.api.Common.Api;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;

namespace Financas.api.EndPoints.Categories
{
    public class DeleteTransactionEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}", HadleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma Transação")
            .WithDescription("Exclui uma Transação")
            .WithOrder(3)
            .Produces<Response<Transaction>>();
    private static async Task<IResult> HadleAsync(ITransactionHandler handler, long id)
        {

            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfiguration.UserId ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteTransactionAsync(request);
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
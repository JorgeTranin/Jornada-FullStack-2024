using Financas.core.Models;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;

namespace Financas.core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateTransactionAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateTransactionAsync(UpdateTransactionRequest request);
        Task<PagedResponse<List<Transaction>?>> GetTransactionByPeriodAsync(GetTransactionByPeriodRequest request);
        Task<Response<Transaction?>> GetTransactionByIdAsync(GetTransactionByIdRequest request);
        Task<Response<Transaction?>> DeleteTransactionAsync(DeleteTransactionRequest request);
    }
}
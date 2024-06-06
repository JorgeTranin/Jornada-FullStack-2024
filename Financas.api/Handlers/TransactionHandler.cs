using Financas.api.Data;
using Financas.core.Commoun;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Transactions;
using Financas.core.Responses;
using Microsoft.EntityFrameworkCore;


namespace Financas.api.Handlers
{
    public class TransactionHandler(AppDbContext dbContext) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateTransactionAsync(CreateTransactionRequest request)
        {
            if (request is { Type: core.Enums.ETransactionType.exit, Amount: >= 0 })
            {
                request.Amount *= -1;
            }


            Transaction transaction = new()
            {
                Title = request.Title,
                Type = request.Type,
                CreateAt = DateTime.Now,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Category = request.Category,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                UserId = request.UserId
            };
            try
            {
                await dbContext.Transactions.AddAsync(transaction);
                await dbContext.SaveChangesAsync();
                return new Response<Transaction?>(transaction, code: 201, message: "Sucesso no cadastro da trasação");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro no cadastro da categoria");
            }
        }

        public async Task<Response<Transaction?>> DeleteTransactionAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transactionDb = await dbContext.Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (transactionDb is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }
                else
                {
                    dbContext.Transactions.Remove(transactionDb);
                    await dbContext.SaveChangesAsync();
                    return new Response<Transaction?>(null, 204, "Transação excluida com sucesso");
                }
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro no cadastro da categoria");
            }
        }

        public async Task<Response<Transaction?>> GetTransactionByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transactionDb = await dbContext.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (transactionDb is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }
                else
                {
                    return new Response<Transaction?>(transactionDb, message: "Transação Atualizada com Sucesso");
                }
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro na busca da Transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetTransactionByPeriodAsync(GetTransactionByPeriodRequest request)
        {

            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDayMounth();
                request.EndDate ??= DateTime.Now.GetLastDayMounth();

                var query = dbContext.Transactions
                .AsNoTracking()
                .Where(x =>
                 x.PaidOrReceivedAt >= request.StartDate &&
                 x.PaidOrReceivedAt <= request.EndDate &&
                 x.UserId == request.UserId)
                .OrderBy(x => x.PaidOrReceivedAt);


                var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Erro na busca das transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateTransactionAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: core.Enums.ETransactionType.exit, Amount: >= 0 })
            {
                request.Amount *= -1;
            }



            try
            {
                var transactionDb = await dbContext.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transactionDb is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }
                else
                {
                    transactionDb.Title = request.Title;
                    transactionDb.Type = request.Type;
                    transactionDb.Amount = request.Amount;
                    transactionDb.CategoryId = request.CategoryId;
                    transactionDb.Category = request.Category;
                    transactionDb.PaidOrReceivedAt = request.PaidOrReceivedAt;

                    dbContext.Transactions.Update(transactionDb);
                    await dbContext.SaveChangesAsync();
                    return new Response<Transaction?>(transactionDb, code: 201, message: "Sucesso no Update da trasação");
                }

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro no update da transação");
            }
        }
    }
}
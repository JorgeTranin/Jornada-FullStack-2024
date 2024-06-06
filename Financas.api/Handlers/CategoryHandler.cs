
using Financas.api.Data;
using Financas.core.Handlers;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Financas.api.Handlers
{
    public class CategoryHandler(AppDbContext dbContext) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            Category category = new()
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
            };
            try
            {

                await dbContext.Categories.AddAsync(category);
                await dbContext.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria Criada com Sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro no cadastro da categoria");
            }


        }

        public async Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            try
            {
                var categoryDb = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (categoryDb is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }
                else
                {
                    dbContext.Categories.Remove(categoryDb);
                     await dbContext.SaveChangesAsync();
                    return new Response<Category?>(null, 204, "Categoria excluida com sucesso");
                }
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro na exclusão da categoria");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllCategoryAsync(GetAllCategoryRequest request)
        {
            try
            {
                var query = dbContext.Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);


                var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>?>(null, 500, "Erro na busca das categoria");
            }
        }

        public async Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var categoryDb = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (categoryDb == null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }
                else
                {
                    return new Response<Category?>(categoryDb, message: "Categoria Atualizada com Sucesso");
                }
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro na busca da categoria");
            }
        }

        public async Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            try
            {
                var categoryDb = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (categoryDb == null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }
                else
                {

                    categoryDb.Title = request.Title;
                    categoryDb.Description = request.Description;


                    dbContext.Categories.Update(categoryDb);
                    await dbContext.SaveChangesAsync();

                    return new Response<Category?>(categoryDb, message: "Categoria Atualizada com Sucesso");
                }
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro no update da categoria");
            }
        }
    }
}
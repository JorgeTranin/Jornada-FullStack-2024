using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financas.core.Models;
using Financas.core.Requests.Categories;
using Financas.core.Responses;

namespace Financas.core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<PagedResponse<List<Category>?>> GetAllCategoryAsync(GetAllCategoryRequest request);
        Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request);
        Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request);
    }
}
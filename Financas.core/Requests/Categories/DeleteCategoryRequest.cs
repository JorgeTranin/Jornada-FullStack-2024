using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        public long Id { get; set; }
    }
}
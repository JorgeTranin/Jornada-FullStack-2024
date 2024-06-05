using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Financas.core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Título invalido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter menos que 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição Invalida")]
        public string Description { get; set; } = string.Empty;

    }
}
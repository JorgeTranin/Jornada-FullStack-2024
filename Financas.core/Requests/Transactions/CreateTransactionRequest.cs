using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Financas.core.Enums;
using Financas.core.Models;

namespace Financas.core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
    

        [Required(ErrorMessage = "Título invalido")]
        [MaxLength(80, ErrorMessage = "O Titulo deve conter no maximo 80 Caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor invalido")]
        public ETransactionType Type { get; set; } = ETransactionType.entry;

        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Valor invalido")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Valor invalido")]
        public Category? Category { get; set; } = null;

        [Required(ErrorMessage = "Valor invalido")]
        public DateTime? PaidOrReceivedAt { get; set; }

    }
}
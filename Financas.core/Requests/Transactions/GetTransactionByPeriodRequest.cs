using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.core.Requests.Transactions
{
    public class GetTransactionByPeriodRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; }   
        public DateTime? EndDate { get; set; }   
    }
}
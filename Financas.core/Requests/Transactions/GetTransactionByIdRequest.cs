using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Request
    {
        public long Id{ get; set; }
    }
}
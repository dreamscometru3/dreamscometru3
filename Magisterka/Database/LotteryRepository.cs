using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Database
{
    using Magisterka.Models;

    public class LotteryRepository : GenericRepository<LotteryTicket>
    {
        public LotteryRepository(Entities context)
            : base(context)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Models
{
    public class CoinTossResult : BaseModel
    {
        public int Result { get; set; }

        public virtual ClientCodeModel ClientCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Database
{
    using System.Data.Entity;

    using Magisterka.Models;

    public class Entities : DbContext
    {
        public IDbSet<ClientCodeModel> ClientCode { get; set; }
        public IDbSet<CoinTossResult> CoinToss { get; set; }
        public IDbSet<LogicGameResult> LogicGame { get; set; }
        public IDbSet<LotteryTicket> LotteryTickets { get; set; }
        public IDbSet<SurveyModel> SurveyModels { get; set; } 
    }
}
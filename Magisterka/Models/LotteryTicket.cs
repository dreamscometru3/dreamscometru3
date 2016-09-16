namespace Magisterka.Models
{
    public class LotteryTicket : BaseModel
    {
        public int UserTickets { get; set; }

        public virtual ClientCodeModel ClientCode { get; set; }
    }
}
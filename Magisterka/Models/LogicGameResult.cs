namespace Magisterka.Models
{
    public class LogicGameResult : BaseModel
    {
        public WinStatus WinStatus { get; set; }

        public virtual ClientCodeModel ClientCode { get; set; }
    }

    public enum WinStatus
    {
        w50,
        w51,
        w54
    }
}
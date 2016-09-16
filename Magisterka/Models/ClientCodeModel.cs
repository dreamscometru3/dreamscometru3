namespace Magisterka.Models
{
    using System;

    public class ClientCodeModel : BaseModel
    {
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CodeStatus Status { get; set; }
    }

    public enum CodeStatus
    {
        NotStarted,
        Started,
        Completed
    }
}
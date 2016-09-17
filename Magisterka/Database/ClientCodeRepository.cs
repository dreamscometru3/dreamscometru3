namespace Magisterka.Database
{
    using System.Linq;

    using Magisterka.Models;

    public class ClientCodeRepository : GenericRepository<ClientCodeModel>
    {
        public ClientCodeRepository(Entities context)
            : base(context)
        {
        }

        public ClientCodeModel GetByCode(string code)
        {
            return this.GetAll().FirstOrDefault(x => x.Code == code);
        }

        public void UpdateStatus(ClientCodeModel clientCode)
        {
            if (clientCode.Status == CodeStatus.NotStarted)
            {
                clientCode.Status = CodeStatus.Started;
            }

            this.UpdateCreate(clientCode);
        }
    }

}
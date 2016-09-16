using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magisterka.Database
{
    public class Repository
    {
        protected readonly Entities dbContext;
        private bool disposed = false;

        public ClientCodeRepository ClientCodeRepository { get { return new ClientCodeRepository(this.dbContext);} }

        public Repository()
        {
            this.dbContext = new Entities();
        }

        public Repository(Entities entities)
        {
            this.dbContext = entities;
        }
    }
}
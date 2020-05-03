using Model.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataAccess.DatabaseContext
{
    public class UnitOfWork : IDisposable
    {        
        private GenericRepository<Channel> channelRepository;        
        ModelArchContext _dbContext;

        public UnitOfWork(ModelArchContext context)
        {
            _dbContext = context;
        }
              

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

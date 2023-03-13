using Door2DoorLib.Interfaces;
using Door2DoorLib.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Door2DoorLib.Repositories
{
    internal class DbLogRepository : IDbLogRepository
    {
        private readonly IDatabase _database;

        public DbLogRepository(IDatabase database)
        {
            _database = database;
        }


        public Task<bool> CreateAsync(DatabaseLog createEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(DatabaseLog deleteEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DatabaseLog>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseLog> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(DatabaseLog updateEntity)
        {
            throw new NotImplementedException();
        }
    }
}

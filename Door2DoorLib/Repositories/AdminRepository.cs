using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Door2DoorLib.Repositories
{
    internal class AdminRepository : IAdminRepository
    {
      private IDatabase _database;

        public AdminRepository(IDatabase database)
        {
            _database = database;
        }
    }
}

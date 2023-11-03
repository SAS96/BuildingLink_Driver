using BuildingLink_Driver.Models;
using System.Collections.Generic;

namespace BuildingLink_Driver.Data
{
    public interface IDriversRepository
    {
        List<Driver> Get();
        Driver? Get(int id);
        int Add(Driver driver);
        int Update(Driver driver);
        int Delete(int id);
        void BulkInsert(IEnumerable<Driver> drivers);
    }
}

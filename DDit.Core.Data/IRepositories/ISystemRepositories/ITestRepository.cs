using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
    public interface ITestRepository
    {
        void Delete();

        Test GetSingle(int ID);
    }
}

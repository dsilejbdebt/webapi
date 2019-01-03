using ServerInfoDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WebApiWithCRUD.Persistance
{
    public interface IServerDataRepository
    {
        ServerInfo Get(int id);
        IQueryable<ServerInfo> GetAll();
        void Add(ServerInfo std);
        void Delete(int id);
        bool Update(ServerInfo sd);
        
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServerInfoDataAccess;
using WebApiWithCRUD.Controllers;



namespace WebApiWithCRUD.Persistance
{
    public class ServerDataRepository : IServerDataRepository
    {
        public void Add(ServerInfo std)
        {
            try
            {
                using (var context = new RDConnection())
                {
                   
                    context.ServerInfoSet.Add(std);

                    context.SaveChanges();
                }
                
            }
            catch (DbUpdateException ex)
            {
                ServerDataController.logs.TrackException(ex);

            }
           
        }

        public void Delete(int id)
        {
            try
            {
                using (var context = new RDConnection())
                {
                    var std = context.ServerInfoSet.Find(id);
                    context.ServerInfoSet.Remove(std);

                    context.SaveChanges();
                }
            }

            catch (NullReferenceException ex)
            {
                ServerDataController.logs.TrackException(ex);
            }
        }

        public ServerInfo Get(int id) 
        {
            using (var context = new RDConnection())
            {
                try
                {
                    return context.ServerInfoSet.Single(emp => emp.server_id == id);
                }
                catch (InvalidOperationException ex)
                {
                    ServerDataController.logs.TrackException(ex);
                    return new ServerInfo
                    {
                        server_id = id,
                        name = "info related to id " + id + " not in the database",
                        date = "info related to id " + id + " not in the database",
                        loction = "info related to id " + id + " not in the database",
                        
                        

                    };
                }
            }
        }

        public IQueryable<ServerInfo> GetAll()
        {
            
           var context = new RDConnection();
            return context.ServerInfoSet;

        }

        public bool Update(ServerInfo sd)
        { int status=0;

            try
            { 
                using (var context = new RDConnection())
                {
                    ServerInfo std = context.ServerInfoSet.Find(sd.server_id);

                    std.name = sd.name;
                    std.loction = sd.loction;
                    std.date = sd.date;

                    status=context.SaveChanges();
                
                }
            }

            catch (NullReferenceException ex)
            {
                ServerDataController.logs.TrackException(ex);
                return false;
            }

            if (status == 1)
                return true;
            else
                return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DDit.Core.Data.Repository
{
   public class ConnectDB
    {
       public static DbContext DataBase(){
           
           var dbconfig = ConfigurationManager.AppSettings["DBconnect"].ToString();
           DbContext dbconnect=null;
           switch (dbconfig)
           {
               case "sqlserver":
                   dbconnect=new CoreDbContext();
                   break;
               case "oracle":
                   dbconnect=new OracleDbContext();
                   break;
               default:
                   dbconnect=new CoreDbContext();
                   break;
           }
           return dbconnect;
       }
    }
}

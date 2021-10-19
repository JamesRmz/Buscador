using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    //la clase abstracta no pude ser instanciada solo puede ser utilizada como clase base//
    public abstract class ConnectionToSql
    {
        //solo leectura//
        private readonly string connectionString;
        public ConnectionToSql()
        {
            //Cadena de conexion SQL//
            connectionString = "Server=DESKTOP\\SQLEXPRESS;DataBase=NameDatabase; integrated security = true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
       
    }
}

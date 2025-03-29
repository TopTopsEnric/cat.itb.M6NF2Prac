using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.connections
{
    public class StoreCloudConnection
    {
        private String HOST = "postgresql-enric.alwaysdata.net"; // Ubicació de la BD.
        private String DB = "enric_practica1"; // nom de la BD.
        private String USER = "enric";
        private String PASSWORD = "Truvego_99";

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}

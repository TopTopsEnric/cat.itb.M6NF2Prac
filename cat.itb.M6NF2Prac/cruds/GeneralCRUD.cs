using cat.itb.M6NF2Prac.connections;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class GeneralCRUD
    {
        public void DropTables(List<string> tables)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            foreach (var table in tables)
            {
                var cmd = new NpgsqlCommand("DROP TABLE " + table + " CASCADE", conn);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table {0} succesfully dropped", table);
                }
                catch
                {
                    Console.WriteLine("Table {0} doesn't exist", table);
                }
            }
            conn.Close();
        }

        public void RunScriptHR()
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            string script = File.ReadAllText("../../MyFiles/store.sql");
            var cmd = new NpgsqlCommand(script, conn);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Script executed successfully");
            }
            catch
            {
                Console.WriteLine("Couldn't execute script, try to execute option 12 and then 11 again");
            }
            conn.Close();
        }

        public void RestoreHR2DBSession()
        {
            string path = @"..\..\MyFiles\store.sql";
            string sql = File.ReadAllText(path);
            using (var session = SessionFactoryStoreCloud.Open())
            {
                session.CreateSQLQuery(sql).ExecuteUpdate();
                Console.WriteLine("DB HR2 restored");
            }
        }
    }
}


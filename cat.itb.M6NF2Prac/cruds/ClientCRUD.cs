using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cat.itb.M6NF2Prac.cruds
{
    public class ClientCRUD
    {
        // 🔹 Obtener todos los clientes
        public IList<Client> SelectAll()
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Client>().ToList();
            }
        }

        // 🔹 Obtener un cliente por ID
        public Client SelectById(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Client>().FirstOrDefault(c => c.id == id);
            }
        }

        // 🔹 Insertar un nuevo cliente
        public void Insert(Client client)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(client);
                transaction.Commit();
            }
        }

        // 🔹 Actualizar un cliente existente
        public void Update(Client client)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(client);
                transaction.Commit();
            }
        }

        // 🔹 Eliminar un cliente por ID
        public void Delete(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                var client = session.Query<Client>().FirstOrDefault(c => c.id == id);
                if (client != null)
                {
                    session.Delete(client);
                    transaction.Commit();
                }
            }
        }

        public void InsertAdo(List<Client> clientes)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            foreach (var cliente in clientes)
            {
                string sql = $"INSERT INTO client ( code, name, credit) " +
                             $"VALUES ('{cliente.code}', '{cliente.name}', '{cliente.credit}')";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    int nRows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Clients inserits correctament");
                }
            }

            conn.Close();
        }


        public Client SelectByNameADO(string name)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using var conn = db.GetConnection(); 
            string sql = "SELECT * FROM client WHERE name=@name";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare(); // Usando Prepared Statement

       

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read()) // Si hay resultados
            {
                return new Client
                {
                    id = rdr.GetInt32(rdr.GetOrdinal("id")),
                    code = rdr.GetInt32(rdr.GetOrdinal("code")),
                    name = rdr.GetString(rdr.GetOrdinal("name")),
                    credit = rdr.GetFloat(rdr.GetOrdinal("credit")) 
                };
            }
            else
            {
                Console.WriteLine("No s'han trobat resultats");
                return null;
            }
        }


        public void DeleteADO(Client cliente)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using var conn = db.GetConnection();
            string sql = "delete FROM client WHERE id=@id";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", cliente.id);
            cmd.Prepare(); 
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

           Console.WriteLine($" S’ha eliminat correctament el client amb id = {cliente.id}.");
        }
    }
}

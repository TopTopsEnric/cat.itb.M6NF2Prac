using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cat.itb.M6NF2Prac.cruds
{
    public class ProductCRUD
    {

        // 🔹 Obtener todas las ordenes
        public IList<Product> SelectAll()
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Product>().ToList();
            }
        }

        // 🔹 Obtener un cliente por ID
        public Product SelectById(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Product>().FirstOrDefault(c => c.id == id);
            }
        }

        // 🔹 Insertar un nuevo cliente
        public void Insert(Product producto)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(producto);
                transaction.Commit();
            }
        }

        // 🔹 Actualizar un cliente existente
        public void Update(Product producto)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(producto);
                transaction.Commit();
            }
        }

        // 🔹 Eliminar un cliente por ID
        public void Delete(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                var client = session.Query<Product>().FirstOrDefault(c => c.id == id);
                if (client != null)
                {
                    session.Delete(client);
                    transaction.Commit();
                }
            }
        }

        public Product SelectByCodeADO(int code)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using var conn = db.GetConnection();
            string sql = "SELECT * FROM product WHERE code=@code";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", code);
            cmd.Prepare(); // Usando Prepared Statement



            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read()) // Si hay resultados
            {
                return new Product
                {
                    id = rdr.GetInt32(rdr.GetOrdinal("id")),
                    code = rdr.GetInt32(rdr.GetOrdinal("code")),
                    description = rdr.GetString(rdr.GetOrdinal("description")),
                    currentstock = rdr.GetInt32(rdr.GetOrdinal("currentstock")),
                    minstock = rdr.GetInt32(rdr.GetOrdinal("minstock")),
                    price = rdr.GetFloat(rdr.GetOrdinal("price")),
                    //salesp = rdr.GetInt32(rdr.GetOrdinal("salesp"))
                };
            }
            else
            {
                Console.WriteLine("No s'han trobat resultats");
                return null;
            }
        }

        public void UpdateADO(Product producto)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();
            string sql = "UPDATE product SET price = @price WHERE id = @id";
            
            using (var cmd = new NpgsqlCommand(sql, conn))
                {
                      cmd.Parameters.AddWithValue("price", producto.price);
                      cmd.Parameters.AddWithValue("id", producto.id);
                      int nRows = cmd.ExecuteNonQuery();
                      Console.WriteLine($"Producta modificat correctament");
                }
            
        }


    }
}

using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class SalespersonCRUD
    {

        // 🔹 Obtener todas las ordenes
        public IList<Salesperson> SelectAll()
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Salesperson>().ToList();
            }
        }

        // 🔹 Obtener un cliente por ID
        public Salesperson SelectById(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Salesperson>().FirstOrDefault(c => c.id == id);
            }
        }

        // 🔹 Insertar un nuevo cliente
        public void Insert(Salesperson vendedor)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(vendedor);
                transaction.Commit();
            }
        }

        // 🔹 Actualizar un cliente existente
        public void Update(Salesperson vendedor)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(vendedor);
                transaction.Commit();
            }
        }

        // 🔹 Eliminar un cliente por ID
        public void Delete(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                var client = session.Query<Salesperson>().FirstOrDefault(c => c.id == id);
                if (client != null)
                {
                    session.Delete(client);
                    transaction.Commit();
                }
            }
        }

        public void InsertAdo(List<Salesperson> vendedores)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            foreach (var vendedor in vendedores)
            {
                // Usamos un parámetro en la consulta para evitar problemas con la fecha
                string sql = "INSERT INTO salesperson (surname, job, startdate, salary, commission, dep) " +
                             "VALUES (@surname, @job, @startdate, @salary, @commission, @dep)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@surname", vendedor.surname);
                    cmd.Parameters.AddWithValue("@job", vendedor.job);
                    cmd.Parameters.AddWithValue("@startdate", vendedor.startdate);
                    cmd.Parameters.AddWithValue("@salary", vendedor.salary);
                    cmd.Parameters.AddWithValue("@commission", (object)vendedor.commission ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dep", vendedor.dep);

                    // Ejecutar la consulta
                    int nRows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Vendedor {vendedor.surname} insertado correctamente.");
                }
            }

            conn.Close();
        }

        public Salesperson SelectBySurname(string surname)
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            {
                // Utilizamos HQL para buscar el vendedor por apellido
                string hql = "FROM Salesperson s WHERE s.surname = :surname";
                IQuery query = session.CreateQuery(hql);
                query.SetParameter("surname", surname);

                // Devolvemos el primer vendedor que coincida con el apellido
                return query.UniqueResult<Salesperson>();
            }
        }
    }
}

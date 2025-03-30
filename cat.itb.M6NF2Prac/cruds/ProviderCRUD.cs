using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate.Criterion;
using NHibernate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class ProviderCRUD
    {

        // 🔹 Obtener todas las ordenes
        public IList<Provider> SelectAll()
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Provider>().ToList();
            }
        }

        // 🔹 Obtener un cliente por ID
        public Provider SelectById(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Provider>().FirstOrDefault(c => c.id == id);
            }
        }

        public IList<Provider> SelectByCity(string city)
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            {
                // Utilizamos HQL para buscar proveedores por ciudad
                string hql = "FROM Provider p WHERE p.city = :city";
                IQuery query = session.CreateQuery(hql);
                query.SetParameter("city", city);

                return query.List<Provider>();
            }
        }

        public bool UpdateHQL(Provider provider)
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Update(provider);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }


        // 🔹 Actualizar un cliente existente
        public void Update(Provider proveedor)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(proveedor);
                transaction.Commit();
            }
        }

        // 🔹 Eliminar un cliente por ID
        public void Delete(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                var client = session.Query<Provider>().FirstOrDefault(c => c.id == id);
                if (client != null)
                {
                    session.Delete(client);
                    transaction.Commit();
                }
            }
        }

        public List<Provider> SelectCreditLowerThanADO(float maxCredit)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            List<Provider> providers = new List<Provider>();

            try
            {
                // Construcción de la consulta sin parámetros preparados
                string sql = $"SELECT * FROM provider WHERE credit < {maxCredit}";

                using var cmd = new NpgsqlCommand(sql, conn);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Provider provider = new Provider
                    {
                        id = rdr.GetInt32(0),
                        name = rdr.GetString(1),
                        address = rdr.GetString(2),
                        city = rdr.GetString(3),
                        stcode = rdr.GetString(4),
                        zipcode = rdr.GetString(5),
                        area = rdr.GetInt32(6),
                        phone = rdr.GetString(7),
                        // Si `product` es un objeto, necesitarás hacer una consulta separada o modificar esto
                        amount = rdr.GetInt32(9),
                        credit = rdr.GetFloat(10),
                        remark = rdr.GetString(11)
                    };

                    providers.Add(provider);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return providers;
        }


        public Provider SelectLowestAmount()
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            {
                // Utilizamos QueryOver con subquery para encontrar el proveedor con la cantidad mínima
                Provider providerAlias = null;

                // Subconsulta para obtener la cantidad mínima
                QueryOver<Provider> subQuery = QueryOver.Of<Provider>()
                    .Select(Projections.Min<Provider>(x => x.amount));

                // Consulta principal que busca el proveedor cuya cantidad sea igual a la mínima
                var result = session.QueryOver<Provider>(() => providerAlias)
                    .WithSubquery.WhereProperty(() => providerAlias.amount)
                    .Eq(subQuery)
                    .SingleOrDefault<Provider>();

                return result;
            }
        }


        public void Insert(Provider provider)
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Save(provider);
                    transaction.Commit();
                    Console.WriteLine($"Proveedor con ID {provider.id} insertado correctamente");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al insertar proveedor: {ex.Message}");
                    throw;
                }
            }
        }
    }
}

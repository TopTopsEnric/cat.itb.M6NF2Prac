using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
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

        // 🔹 Insertar un nuevo cliente
        public void Insert(Provider proveedor)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(proveedor);
                transaction.Commit();
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
    }
}

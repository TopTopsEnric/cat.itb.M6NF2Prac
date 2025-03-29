using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
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
    }
}

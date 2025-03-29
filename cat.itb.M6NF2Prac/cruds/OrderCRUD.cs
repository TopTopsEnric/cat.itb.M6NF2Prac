using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class OrderCRUD
    {
        
            // 🔹 Obtener todas las ordenes
            public IList<Order> SelectAll()
            {
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    return session.Query<Order>().ToList();
                }
            }

            // 🔹 Obtener un cliente por ID
            public Order SelectById(int id)
            {
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    return session.Query<Order>().FirstOrDefault(c => c.id == id);
                }
            }

            // 🔹 Insertar un nuevo cliente
            public void Insert(Order orden)
            {
                using (var session = SessionFactoryStoreCloud.Open())
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(orden);
                    transaction.Commit();
                }
            }

            // 🔹 Actualizar un cliente existente
            public void Update(Order orden)
            {
                using (var session = SessionFactoryStoreCloud.Open())
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(orden);
                    transaction.Commit();
                }
            }

            // 🔹 Eliminar un cliente por ID
            public void Delete(int id)
            {
                using (var session = SessionFactoryStoreCloud.Open())
                using (var transaction = session.BeginTransaction())
                {
                    var client = session.Query<Order>().FirstOrDefault(c => c.id == id);
                    if (client != null)
                    {
                        session.Delete(client);
                        transaction.Commit();
                    }
                }
            }
        
    }
}

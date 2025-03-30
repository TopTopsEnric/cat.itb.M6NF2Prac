using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate;
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
        public IList<Orders> SelectAll()
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Orders>().ToList();
            }
        }

        // 🔹 Obtener un cliente por ID
        public Orders SelectById(int id)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                return session.Query<Orders>().FirstOrDefault(c => c.id == id);
            }
        }

        // 🔹 Insertar un nuevo cliente
        public void Insert(Orders orden)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(orden);
                transaction.Commit();
            }
        }

        // 🔹 Actualizar un cliente existente
        public void Update(Orders orden)
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
                var client = session.Query<Orders>().FirstOrDefault(c => c.id == id);
                if (client != null)
                {
                    session.Delete(client);
                    transaction.Commit();
                }
            }
        }

        public IList<Orders> SelectByCostHigherThan(decimal cost, int amount)
        {
            using (ISession session = SessionFactoryStoreCloud.Open())
            {
                // Utilizamos Criteria para buscar las comandas que cumplan las condiciones
                ICriteria criteria = session.CreateCriteria<Orders>()
                    .Add(Restrictions.Gt("cost", cost))  
                    .Add(Restrictions.Eq("amount", amount));

                // Especificamos que también queremos cargar los productos relacionados
                criteria.CreateAlias("product", "p", JoinType.InnerJoin);

                // Devolvemos la lista de comandas que cumplen los criterios
                return criteria.List<Orders>();
            }
        }

    }
}

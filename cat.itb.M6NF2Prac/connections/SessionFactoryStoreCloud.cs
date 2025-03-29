using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6NF2Prac.model;

namespace cat.itb.M6NF2Prac.connections
{
    public class SessionFactoryStoreCloud
    {

        private static string ConnectionString = "Server=postgresql-enric.alwaysdata.net;Port=5432;Database=enric_practica1;User Id=enric;Password=Truvego_99;";
        private static ISessionFactory _session;

        public static ISessionFactory CreateSession()
        {
            if (_session != null) return _session;

            IPersistenceConfigurer configDb = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);
            FluentConfiguration configMap = Fluently.Configure().Database(configDb)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Client>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Provider>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Salesperson>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Order>());

            _session = configMap.BuildSessionFactory();

            return _session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}

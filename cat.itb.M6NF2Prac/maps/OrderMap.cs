using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    internal class OrderMap: ClassMap<Order>
    {
        public OrderMap()
        {
            Table("orderprod");
            Id(x => x.id);
            Map(x => x.orderdate).Column("orderdate");
            Map(x => x.amount).Column("amount");
            Map(x => x.deliverydate).Column("deliverydate");
            Map(x => x.cost).Column("cost");
            References(x => x.product)
                .Column("product")
                .Not.LazyLoad()
                .Fetch.Join();
            References(x => x.client)
                .Column("client")
                .Not.LazyLoad()
                .Fetch.Join();
        }
    }
}

using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    internal class ProductMap: ClassMap<Product>
    {
        public ProductMap() {

            Table("product");
            Id(x => x.id);
            Map(x => x.code).Column("code");
            Map(x => x.description).Column("description");
            Map(x => x.currentstock).Column("currentstock");
            Map(x => x.minstock).Column("minstock");
            Map(x => x.price).Column("price");
            References(x => x.salesp)
                .Column("salesp")
                .Not.LazyLoad()
                .Fetch.Join();
            HasMany(x => x.ordenes)
                 .AsSet()
                 .KeyColumn("product")
                 .Not.LazyLoad()
                 .Cascade.AllDeleteOrphan()
                 .Fetch.Join();
            HasOne(x => x.prov)
              .PropertyRef("product")
              .Not.LazyLoad()
               .Cascade.None();
               //.Nullable(); // Permite valores nulos


        }
    }
}

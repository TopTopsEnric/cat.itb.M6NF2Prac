using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    internal class ProviderMap: ClassMap<Provider>
    {
        public ProviderMap() {


            Table("provider");
            Id(x => x.id).GeneratedBy.Identity(); 
            Map(x => x.name).Column("name");
            Map(x => x.address).Column("address");
            Map(x => x.city).Column("city");
            Map(x => x.stcode).Column("stcode");
            Map(x => x.zipcode).Column("zipcode");
            Map(x => x.area).Column("area");
            Map(x => x.phone).Column("phone");
            Map(x => x.amount).Column("amount");
            Map(x => x.credit).Column("credit");
            Map(x => x.remark).Column("remark");
            HasOne(x => x.product).Constrained();
        }
    }
}

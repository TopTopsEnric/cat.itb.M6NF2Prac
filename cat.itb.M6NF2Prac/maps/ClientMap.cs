using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    internal class ClientMap: ClassMap<Client>
    {
        public ClientMap()
        {

            Table("client");
            Id(x => x.id);
            Map(x => x.code).Column("code");
            Map(x => x.name).Column("name");
            Map(x => x.credit).Column("credit");
            HasMany(x => x.comandes)
                 .AsSet()
                 .KeyColumn("client")
                 .Not.LazyLoad()
                 .Cascade.AllDeleteOrphan()
                 .Fetch.Join();
        }
    }
}

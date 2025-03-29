using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    internal class SalespersonMap: ClassMap<Salesperson>
    {
        public SalespersonMap() {

            Table("salesperson");
            Id(x => x.id);
            Map(x => x.surname).Column("surname");
            Map(x => x.job).Column("job");
            Map(x => x.startdate).Column("startdate");
            Map(x => x.salary).Column("salary");
            Map(x => x.commission).Column("comission");
            Map(x => x.dep).Column("dep");
            HasMany(x => x.product)
                 .AsSet()
                 .KeyColumn("salesp")
                 .Not.LazyLoad()
                 .Cascade.AllDeleteOrphan()
                 .Fetch.Join();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public  class Order
    {
        public virtual int id { get; set; }

        public virtual Product product { get; set; }
        public virtual Client client { get; set; }
        public virtual DateTimeOffset orderdate { get; set; }
        public virtual int amount { get; set; }
        public virtual DateTimeOffset deliverydate { get; set; }
        public virtual float cost { get; set; }
    }
}

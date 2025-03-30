using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public  class Orders
    {
        public virtual int id { get; set; }

        public virtual Product product { get; set; }
        public virtual Client client { get; set; }
        public virtual DateTime orderdate { get; set; }
        public virtual int amount { get; set; }
        public virtual DateTime deliverydate { get; set; }
        public virtual decimal cost { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public class Client
    {
        public virtual int id { get; set; }
        public virtual int code { get; set; }
        public virtual string name { get; set; }
        public virtual decimal credit { get; set; }

        public virtual ISet<Orders> comandes { get; set; }

    }
}

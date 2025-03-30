using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public class Salesperson
    {
        public virtual int id { get; set; }
        
        public virtual string surname { get; set; }
        public virtual string job { get; set; }
        public virtual DateTime startdate { get; set; }
        public virtual float salary { get; set; }
        public virtual float? commission { get; set; }
        public virtual string dep { get; set; }

        public virtual ISet<Product> product { get; set; }

    }
}

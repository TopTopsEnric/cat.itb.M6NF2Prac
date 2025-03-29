using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public class Provider
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string address { get; set; }
        public virtual string city { get; set; }
        public virtual string stcode { get; set; }
        public virtual string zipcode { get; set; }
        public virtual int area { get; set; }
        public virtual string phone { get; set; }
        public virtual Product product { get; set; }
        public virtual int amount { get; set; }
        public virtual float credit { get; set; }
        public virtual string remark { get; set; }

    }
}

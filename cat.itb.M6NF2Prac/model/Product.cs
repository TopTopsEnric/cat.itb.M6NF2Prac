using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public  class Product
    {
        public virtual int id { get; set; }
        public virtual int code { get; set; }
        public virtual string description { get; set; }
        public virtual int currentstock { get; set; }
        public virtual int minstock { get; set; }
        public virtual double price { get; set; }
        public virtual Salesperson salesp { get; set; }
        public virtual Provider prov { get; set; }


        public virtual ISet<Order> ordenenes { get; set; }


    }
}

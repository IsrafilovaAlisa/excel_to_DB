using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace table.ViewModels
{
    internal class purchase
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public human human { get; set; }
        public DateTime buy_date { get; set; }
    }
}

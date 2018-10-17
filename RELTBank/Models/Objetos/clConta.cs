using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models
{
    public class clConta
    {
        public int conta { get; set; }
        public int agencia { get; set; }
        public double saldo { get; set; }
        public int tipo { get; set; }
        public string status { get; set; }
        public DateTime desat_data { get; set; }
    }
}
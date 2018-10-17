using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models
{
    public class clPessoa
    {
        public clCliente cliente { get; set; }
        public clConta conta { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Retornos
{
    public class clRetornoAbreConta
    {
        public int status { get; set; }
        public string mensagem { get; set; }
        public clConta conta { get; set; }
    }
}
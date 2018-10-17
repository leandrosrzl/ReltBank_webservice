using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RELTBank.Models.Objetos;

namespace RELTBank.Models.Retornos
{
    public class clRetornoExtrato
    {
        public int status { get; set; }
        public List<clExtrato> extrato { get; set; }
    }
}
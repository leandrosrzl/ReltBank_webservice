using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Objetos
{
    public class clExtrato
    {
        public int idmov { get; set; }
        public double valor { get; set; }
        public int agenciaOrigem { get; set; }
        public int contaOrigem { get; set; }
        public int agenciaDestino { get; set; }
        public int contaDestino { get; set; }
        public string historico { get; set; }
        public DateTime dataOperacao { get; set; }
        public DateTime dataCompensado { get; set; }
    }
}
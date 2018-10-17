using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models
{
    public class clCliente
    {
        public int idcli { get; set; }
        public string nome { get; set; }
        public string doc { get; set; }
        public string rg_ie { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string status { get; set; }
        public DateTime datacad { get; set; }
        public DateTime desat_data { get; set; }
    }
}
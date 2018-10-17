using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Retornos
{
    public class clRetornoFavorecido
    {
        public int status { get; set; }
        public Models.Objetos.clFavorecido favorecido { get; set; }
    }
}
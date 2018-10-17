using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RELTBank.Models.Retornos;
using RELTBank.Models.Objetos;
using RELTBank.Models.Consultas.Clientes;

namespace RELTBank.Controllers
{
    public class SaldoController : ApiController
    {
        clRetornoSaldo retSaldo;
        clConsultaSaldo consSaldo;

        /*// GET: api/Saldo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/Saldo/5
        public Models.Retornos.clRetornoSaldo Get(int agencia, int conta, int idusu)
        {
            try
            {
                retSaldo = new clRetornoSaldo();
                consSaldo = new clConsultaSaldo();
                retSaldo.saldo = consSaldo.consultaSaldo(agencia, conta, idusu);
                retSaldo.status = 1;

            }
            catch (Exception)
            {
                retSaldo.status = 0;
            }
            return retSaldo;
        }

        /*// POST: api/Saldo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Saldo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Saldo/5
        public void Delete(int id)
        {
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RELTBank.Models.Retornos;
using RELTBank.Models.Consultas.Clientes;

namespace RELTBank.Controllers
{
    public class ExtratoController : ApiController
    {
        clRetornoExtrato retExtrato;
        /*// GET: api/Extrato
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/Extrato/5
        
        public clRetornoExtrato Get(int agencia, int conta, DateTime dataInicio, DateTime dataFim)
        {
            DataTable dados = new DataTable();
            retExtrato = new clRetornoExtrato();
            clConsultaExtrato consultaExtrato = new clConsultaExtrato();

            retExtrato.status = 1; //Sucesso
            retExtrato.extrato = consultaExtrato.consultaExtrato(agencia, conta, dataInicio, dataFim);
            //retExtrato.
            return retExtrato;
        }

        // POST: api/Extrato
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Extrato/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Extrato/5
        public void Delete(int id)
        {
        }
    }
}

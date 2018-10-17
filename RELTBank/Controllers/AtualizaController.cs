using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RELTBank.Models.Retornos;
using RELTBank.Models.Atualiza;

namespace RELTBank.Controllers
{
    public class AtualizaController : ApiController
    {
        // GET: api/Atualiza
        public clRetornoPost Get()
        //public string Get()
        {
            clRetornoPost retorno = new clRetornoPost();
            clAtualiza atualiza = new clAtualiza();

            try
            {
                retorno.mensagem  = atualiza.atualizarSaldos().ToString();
                retorno.mensagem += " Contas atualizadas";
                retorno.status    = 1;
            }
            catch (Exception ex)
            {
                retorno.mensagem = ex.Message;
                retorno.status   = 0;
            }

            return retorno;
        }

        //// GET: api/Atualiza/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Atualiza
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Atualiza/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Atualiza/5
        //public void Delete(int id)
        //{
        //}
    }
}

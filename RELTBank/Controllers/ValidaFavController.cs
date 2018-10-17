using RELTBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RELTBank.Controllers
{
    public class ValidaFavController : ApiController
    {
        Models.Consultas.Favorecidos.clFavorecidos Favorecidos;
        Models.Retornos.clRetornoFavorecido retFav;

        // GET: api/ValidaFav
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/ValidaFav/5
        public Models.Retornos.clRetornoFavorecido Get(int agencia, int conta)
        {
            try
            {
                retFav = new Models.Retornos.clRetornoFavorecido();
                Favorecidos = new Models.Consultas.Favorecidos.clFavorecidos();
                retFav.favorecido = Favorecidos.consultarFavorecido(agencia, conta,null);
                retFav.status = 1;
            }
            catch (Exception)
            {
                retFav.status = 0;
            }
            return retFav;
        }

        // POST: api/ValidaFav
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ValidaFav/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidaFav/5
        public void Delete(int id)
        {
        }
    }
}

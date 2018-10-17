using RELTBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace RELTBank.Controllers
{
    public class LoginController : ApiController
    {
        clLogar logar;
        clRetornoPessoa retPes;

        //private void pvLogar(int agencia, int conta, string senha)
        //{
        //    logar = new clLogar();
        //    logar.logar(agencia, conta, senha);
        //}



        //-----------------------------------------Métodos de consumo do Web Service--------------------------------------------------
        
        // GET: api/Login/5
        public clRetornoPessoa Get(int agencia, int conta, string senha)
        {
            logar = new clLogar();
            try
            {
                retPes = new clRetornoPessoa();
                retPes.pessoa = logar.logar(agencia, conta, senha);
                retPes.status = 1;
            }
            catch (Exception)
            {

                retPes.status = 0;
            }

            return retPes;
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}

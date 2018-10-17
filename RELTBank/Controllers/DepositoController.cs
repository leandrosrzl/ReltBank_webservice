using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RELTBank.Models.Consultas.Favorecidos;
using RELTBank.Models.Retornos;
using RELTBank.Models.Transferencias;
using RELTBank.Models.Consultas.Clientes;
using RELTBank.Controllers;


namespace RELTBank.Controllers
{
    public class DepositoController : ApiController
    {
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //                                                          PARAMETROS WEB SERVICE
        //----------------------------------------------------------------------------------------------------------------------------------------------

        clRetornoPost retPost;
        clRetornoFavorecido retFav;
        ValidaFavController validaFav;
        TransferenciaController transferencia;

        //----------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      MÉTODOS ADICIONAIS WEB SERVICE
        //----------------------------------------------------------------------------------------------------------------------------------------------


        //// GET: api/Deposito
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Deposito/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Deposito
        public clRetornoPost Post(Double valor, int agencia_favorecido, int conta_favorecido, string nome_favorecido)
        {
            retPost = new clRetornoPost();
            if (valor > 0) //verifica se o valor é positivo
            {
                validaFav     = new ValidaFavController();
                retFav        = new clRetornoFavorecido();
                transferencia = new TransferenciaController();

                retFav = validaFav.Get(agencia_favorecido, conta_favorecido);

                if (retFav.status == 1) //verifica se o favorecido existe
                {
                    transferencia.pvInserirMovimentacao(0, 0, 0, valor, agencia_favorecido, conta_favorecido, nome_favorecido, "DEPOSITOS CAIXA", DateTime.Now);
                    //pvDebitarPagador(idcli_origem, agencia_origem, conta_origem, valor);
                    retPost.status = 1;
                    retPost.mensagem = "Depósito efetuado com sucesso";
                }
                else //caso não exista o favorecido retorna o status 4
                {
                    retPost.status = 4;
                    retPost.mensagem = "Favorecido alterado durante o processo";
                }
            }
            else //caso o valor seja negativo retorna o status 2
            {
                retPost.status = 2;
                retPost.mensagem = "Valor negativo ou igual a zero";
            }

            return retPost;
        }

        //// PUT: api/Deposito/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Deposito/5
        //public void Delete(int id)
        //{
        //}
    }
}

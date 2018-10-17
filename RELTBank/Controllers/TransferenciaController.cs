using RELTBank.Models;
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

namespace RELTBank.Controllers
{
    public class TransferenciaController : ApiController
    {
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //                                                          PARAMETROS WEB SERVICE
        //----------------------------------------------------------------------------------------------------------------------------------------------

        clRetornoPost retPost;
        clInserirMovimentacao movimentacao;
        clConsultaSaldo consultarSaldo;
        clFavorecidos Favorecidos;
        clRetornoFavorecido retFav;
        clDebitarPagador debitarPagador;

        //----------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      MÉTODOS ADICIONAIS WEB SERVICE
        //----------------------------------------------------------------------------------------------------------------------------------------------

        private bool pvConsultaSaldo(int agencia, int conta, int idcli, Double valor)
        {
            Double saldo = 0;
            try
            {
                consultarSaldo = new Models.Consultas.Clientes.clConsultaSaldo();
                saldo = consultarSaldo.consultaSaldo(agencia, conta, idcli);
                if(saldo >= valor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool pvValidaFav(int agencia_favorecido, int conta_favorecido, string nome_favorecido)
        {
            Favorecidos = new clFavorecidos();
            retFav = new clRetornoFavorecido();
            retFav.favorecido = Favorecidos.consultarFavorecido(agencia_favorecido, conta_favorecido, nome_favorecido);
            if(retFav.favorecido.nomeFav == nome_favorecido)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool pvInserirMovimentacao(int idcli_origem, int conta_origem, int agencia_origem, Double valor,
                                           int agencia_favorecido, int conta_favorecido, string nome_favorecido, string historico, DateTime dataOp)
        {
            try
            {
                movimentacao = new clInserirMovimentacao();
                movimentacao.insereMovimentacao(valor, idcli_origem, agencia_origem, conta_origem, 42, agencia_favorecido, conta_favorecido, historico, dataOp);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private bool pvDebitarPagador(int idcli_origem, int agencia, int conta, Double valor)
        {
            try
            {
                debitarPagador = new Models.Transferencias.clDebitarPagador();
                debitarPagador.debitarPagador(idcli_origem, agencia, conta, valor);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void pvCreditarFavorecido()
        {

        }

        /*
         
             SQL PARA RECALCULAR SALDO

                        UPDATE CONTAS SET SALDO = 
                        (
                        --SELECT PARA PEGAR OS VALORES RECEBIDOS (POSITIVOS)
                        (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES 
                        WHERE CONTAS.IDCLI = MOVIMENTACOES.IDCLI_DESTINO AND 
                        CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_DESTINO AND 
                        CONTAS.CONTA = MOVIMENTACOES.CONTA_DESTINO) 
                        
                        -

                        --SELECT PARA PEGAR OS VALORES PAGOS (NEGATIVOS)
                        (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES 
                        WHERE CONTAS.IDCLI = MOVIMENTACOES.IDCLI_ORIGEM AND 
                        CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_ORIGEM AND 
                        CONTAS.CONTA = MOVIMENTACOES.CONTA_ORIGEM)
                        )
             
             
             
             
             */
        //// GET: api/Transferencia
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Transferencia/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        //----------------------------------------------------------------------------------------------------------------------------------------------
        //                                                      MÉTODOS ORIGINAIS WEB SERVICE
        //----------------------------------------------------------------------------------------------------------------------------------------------

        public clRetornoPost Postagem(int idcli_origem, int conta_origem, int agencia_origem, Double valor,
                                                  int agencia_favorecido, int conta_favorecido, string nome_favorecido, DateTime dataOp, string historico)
        {
            retPost = new clRetornoPost();
            if (valor > 0) //verifica se o valor é positivo
            {
                if (pvConsultaSaldo(agencia_origem, conta_origem, idcli_origem, valor)) //verifica se o cliente possui saldo
                {
                    if (pvValidaFav(agencia_favorecido, conta_favorecido, nome_favorecido)) //verifica se o favorecido existe
                    {
                        pvInserirMovimentacao(idcli_origem, conta_origem, agencia_origem, valor, agencia_favorecido, conta_favorecido, nome_favorecido, historico, dataOp);
                        pvDebitarPagador(idcli_origem, agencia_origem, conta_origem, valor);
                        pvCreditarFavorecido();
                        retPost.status = 1;
                        retPost.mensagem = "Transferência efetuada com sucesso";
                    }
                    else //caso não exista o favorecido retorna o status 4
                    {
                        retPost.status = 4;
                        retPost.mensagem = "Favorecido alterado durante o processo";
                    }
                }
                else //caso o cliente não possua saldo retorna o status 3
                {
                    retPost.status = 3;
                    retPost.mensagem = "Saldo insuficiente para efetuar a transação";
                }
            }
            else //caso o valor seja negativo retorna o status 2
            {
                retPost.status = 2;
                retPost.mensagem = "Valor negativo ou igual a zero";
            }

            return retPost;
        }

        // POST: api/Transferencia
        public clRetornoPost Post(int idcli_origem, int conta_origem, int agencia_origem, Double valor, 
                                                  int agencia_favorecido, int conta_favorecido, string nome_favorecido, 
                                                  DateTime dataOp, string historico = "")
        {
            return Postagem(idcli_origem, conta_origem, agencia_origem, valor, agencia_favorecido, conta_favorecido, 
                            nome_favorecido, dataOp, historico);
        }

        // PUT: api/Transferencia/5
        /*public void Put()
        {
        }

        // DELETE: api/Transferencia/5
        public void Delete(int id)
        {
        }*/
    }
}

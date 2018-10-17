using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using RELTBank.Models.Objetos;

namespace RELTBank.Models.Consultas.Clientes
{
    public class clConsultaExtrato
    {
        MySqlCommand comandoMySQL;
        MySqlConnection conexaoMySQL;
        StringBuilder sql;
        DataTable dadosTabela;

        //public List<clExtrato> consultaExtrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim)
        public List<clExtrato> consultaExtrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim)
        {
            comandoMySQL                  = new MySqlCommand();
            sql                           = new StringBuilder();
            dadosTabela                   = new DataTable();
            clExtrato extrato;
            List<clExtrato> listaExtratos = new List<clExtrato>();

            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();
                    
                    sql.Append("SELECT * FROM MOVIMENTACOES WHERE  ");
                    sql.Append("     (AGENCIA_ORIGEM = @AGENCIA OR AGENCIA_DESTINO = @AGENCIA) ");
                    sql.Append(" AND (CONTA_ORIGEM   = @CONTA   OR CONTA_DESTINO   = @CONTA  ) ");
                    sql.Append(" AND (DATA_OPERACAO BETWEEN @DATAINICIO AND @DATAFIM) ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA"   , agencia));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA"     , conta));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@DATAINICIO", dataInicio));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@DATAFIM"   , dataFim));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection  = conexaoMySQL;
                    dadosTabela.Load(comandoMySQL.ExecuteReader());

                    for(int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        extrato = new clExtrato();

                        extrato.idmov = Convert.ToInt32(dadosTabela.Rows[i]["IDMOV"]);
                        extrato.valor = Convert.ToDouble(dadosTabela.Rows[i]["VALOR"]);
                        extrato.agenciaOrigem = Convert.ToInt32(dadosTabela.Rows[i]["AGENCIA_ORIGEM"]);
                        extrato.contaOrigem = Convert.ToInt32(dadosTabela.Rows[i]["CONTA_ORIGEM"]); 
                        extrato.agenciaDestino = Convert.ToInt32(dadosTabela.Rows[i]["AGENCIA_DESTINO"]);
                        extrato.contaDestino = Convert.ToInt32(dadosTabela.Rows[i]["CONTA_DESTINO"]); 
                        extrato.historico = dadosTabela.Rows[i]["HISTORICO"].ToString();
                        extrato.dataOperacao = Convert.ToDateTime(dadosTabela.Rows[i]["DATA_OPERACAO"]);
                        if (dadosTabela.Rows[i]["DATA_COMPENSADO"].ToString() != "")
                        {
                            extrato.dataCompensado = Convert.ToDateTime(dadosTabela.Rows[i]["DATA_COMPENSADO"]);
                        }
                        else
                        {
                            extrato.dataCompensado = Convert.ToDateTime("01/01/1900");
                        }

                        listaExtratos.Add(extrato);
                    }

                    return listaExtratos;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Atualiza
{
    public class clAtualiza
    {
        MySqlConnection conexaoMySQL;
        MySqlCommand    comandoMySQL;
        StringBuilder   sqlInsert   ;
        DataTable       dadosRetorno;

        public void compensaLanctos()
        {
            try
            {
                comandoMySQL = new MySqlCommand();
                sqlInsert = new StringBuilder();
                dadosRetorno = new DataTable();

                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sqlInsert.Append("UPDATE MOVIMENTACOES SET DATA_COMPENSADO = CURRENT_DATE(); ");


                    comandoMySQL.CommandText = sqlInsert.ToString();
                    comandoMySQL.Connection = conexaoMySQL;
                    comandoMySQL.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int atualizarSaldos()
        {
            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    /*
                        SQL PARA RECALCULAR SALDO

                        UPDATE CONTAS SET SALDO = 
                        (
                        -- SELECT PARA PEGAR OS VALORES RECEBIDOS (POSITIVOS)
                        (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES 
                        WHERE CONTAS.CONTA = MOVIMENTACOES.CONTA_DESTINO AND 
                        CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_DESTINO
                        ) 
                        
                        -

                        -- SELECT PARA PEGAR OS VALORES PAGOS (NEGATIVOS)
                        (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES 
                        WHERE CONTAS.CONTA = MOVIMENTACOES.CONTA_ORIGEM AND 
                        CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_ORIGEM
                        )
                        );

                        SELECT ROW_COUNT();
                     */

                    compensaLanctos();

                    conexaoMySQL.Open();

                    int linhas = 0;
                    comandoMySQL = new MySqlCommand();
                    sqlInsert = new StringBuilder();
                    dadosRetorno = new DataTable();

                    sqlInsert.Append("UPDATE CONTAS SET SALDO = ");
                    sqlInsert.Append(" ( ");
                    
                    sqlInsert.Append("    (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES ");
                    sqlInsert.Append("    WHERE CONTAS.CONTA = MOVIMENTACOES.CONTA_DESTINO AND ");
                    sqlInsert.Append("    CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_DESTINO) ");
                    sqlInsert.Append("    - ");

                    sqlInsert.Append("    (SELECT COALESCE(SUM(VALOR), 0.0) FROM MOVIMENTACOES ");
                    sqlInsert.Append("    WHERE CONTAS.CONTA = MOVIMENTACOES.CONTA_ORIGEM AND ");
                    sqlInsert.Append("    CONTAS.AGENCIA = MOVIMENTACOES.AGENCIA_ORIGEM)");
                    sqlInsert.Append("    );");

                    sqlInsert.Append(" SELECT ROW_COUNT(); ");
                    

                    comandoMySQL.CommandText = sqlInsert.ToString();
                    comandoMySQL.Connection  = conexaoMySQL;
                    dadosRetorno.Load(comandoMySQL.ExecuteReader());

                    linhas = Convert.ToInt32(dadosRetorno.Rows[0][0].ToString());

                    return linhas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
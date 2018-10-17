using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace RELTBank.Models.Transferencias
{
    public class clDebitarPagador
    {
        StringBuilder sql;
        MySqlCommand comandoMySQL;
        MySqlConnection conexaoMySQL;

        public void debitarPagador(int idcli, int agencia, int conta, Double valor)
        {
            try
            {
                sql = new StringBuilder();
                comandoMySQL = new MySqlCommand();

                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.OpenAsync();

                    sql.Append("UPDATE CONTAS SET SALDO  = (SALDO - @VALOR) WHERE IDCLI = @IDCLI AND AGENCIA = @AGENCIA AND CONTA = @CONTA");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@VALOR", valor));
                    comandoMySQL.Parameters.Add(new MySqlParameter("IDCLI", idcli));
                    comandoMySQL.Parameters.Add(new MySqlParameter("AGENCIA", agencia));
                    comandoMySQL.Parameters.Add(new MySqlParameter("CONTA", conta));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection = conexaoMySQL;
                    comandoMySQL.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
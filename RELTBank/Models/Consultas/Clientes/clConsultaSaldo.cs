using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace RELTBank.Models.Consultas.Clientes
{
    public class clConsultaSaldo
    {
        MySqlCommand comandoMySQL;
        MySqlConnection conexaoMySQL;
        StringBuilder sql;
        DataTable dadosTabela;

        public Double consultaSaldo(int agencia, int conta, int idcli)
        {
            comandoMySQL = new MySqlCommand ();
            sql          = new StringBuilder();
            dadosTabela  = new DataTable    ();

            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sql.Append("SELECT SALDO FROM CONTAS WHERE AGENCIA = @AGENCIA AND CONTA = @CONTA AND IDCLI = @IDCLI ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA", agencia));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA", conta));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@IDCLI", idcli));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection = conexaoMySQL;
                    dadosTabela.Load(comandoMySQL.ExecuteReader());

                    Double saldo = 0;

                    saldo = Convert.ToDouble(dadosTabela.Rows[0]["SALDO"]);

                    return saldo;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
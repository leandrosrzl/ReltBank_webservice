using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace RELTBank.Models.Transferencias
{
    public class clInserirMovimentacao
    {
        StringBuilder   sql         ;
        DataTable       dadosTabela ;
        MySqlCommand    comandoMySQL;
        MySqlConnection conexaoMySQL;

        public int insereMovimentacao(Double valor, int idcli_origem, int agencia_origem, int conta_origem, 
                                       int idcli_destino, int agencia_destino, int conta_destino, string historico, DateTime dataOp)
        {
            sql          = new StringBuilder();
            dadosTabela  = new DataTable    ();
            comandoMySQL = new MySqlCommand ();

            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sql.Append(" INSERT INTO MOVIMENTACOES (VALOR, IDCLI_ORIGEM, AGENCIA_ORIGEM, CONTA_ORIGEM, " +
                               " IDCLI_DESTINO, AGENCIA_DESTINO, CONTA_DESTINO, DATA_OPERACAO, HISTORICO) ");

                    sql.Append(" VALUES (@VALOR, @IDCLI_ORIGEM, @AGENCIA_ORIGEM, @CONTA_ORIGEM, " +
                               " @IDCLI_DESTINO, @AGENCIA_DESTINO, @CONTA_DESTINO, @DATAOP, @HISTORICO) ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@VALOR"          , valor          ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@IDCLI_ORIGEM"   , idcli_origem   ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA_ORIGEM" , agencia_origem ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA_ORIGEM"   , conta_origem   ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@IDCLI_DESTINO"  , idcli_destino  ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA_DESTINO", agencia_destino));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA_DESTINO"  , conta_destino  ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@DATAOP"         , dataOp         ));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@HISTORICO"      , historico      ));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection  = conexaoMySQL;
                    comandoMySQL.ExecuteNonQuery();

                    return (int) comandoMySQL.LastInsertedId;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
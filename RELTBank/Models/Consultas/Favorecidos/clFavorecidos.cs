using RELTBank.Models;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Consultas.Favorecidos
{
    public class clFavorecidos
    {
        StringBuilder        sql;
        MySqlConnection      conexaoMySQL;
        MySqlCommand         comandoMySQL;
        DataTable            dadosTabela;
        Objetos.clFavorecido Favorecido;

        public Objetos.clFavorecido consultarFavorecido(int agencia, int conta, string favorecido)
        {
            Favorecido   = new Objetos.clFavorecido();
            sql          = new StringBuilder();
            comandoMySQL = new MySqlCommand();
            dadosTabela  = new DataTable();
            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sql.Append(" SELECT CL.NOME FROM CONTAS CO ");
                    sql.Append(" INNER JOIN CLIENTES CL ON CO.IDCLI = CL.IDCLI ");
                    sql.Append(" WHERE CO.STATUS <> 'I' AND CONTA = @CONTA AND AGENCIA = @AGENCIA ");

                    if (favorecido != null)
                    {
                        sql.Append(" AND CL.NOME = @FAVORECIDO;");
                        comandoMySQL.Parameters.Add(new MySqlParameter("@FAVORECIDO", favorecido));
                    }

                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA", conta));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA", agencia));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection = conexaoMySQL;
                    dadosTabela.Load(comandoMySQL.ExecuteReader());

                    Favorecido.agenciaFav = agencia;
                    Favorecido.contaFav = conta;
                    Favorecido.nomeFav = dadosTabela.Rows[0]["NOME"].ToString();
                    return Favorecido;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Cadastro
{
    public class clCadastro
    {
        MySqlConnection conexaoMySQL;
        MySqlCommand comandoMySQL;
        StringBuilder sql;
        DataTable dadosSelect;

        public int cadastrar(string nome, string doc, string rg_ie, string endereco, string numero, string bairro, string cidade, 
                              string estado, string cep, string senha)
        {
            int idcli = 0;

            comandoMySQL = new MySqlCommand();
            sql          = new StringBuilder();
            dadosSelect  = new DataTable();

            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sql.Append("INSERT INTO CLIENTES (NOME, DOC, RG_IE, ENDERECO, NUMERO, BAIRRO, CIDADE, ESTADO, CEP, SENHA) " +
                                             "VALUES (@NOME, @DOC, @RG_IE, @ENDERECO, @NUMERO, @BAIRRO, @CIDADE, @ESTADO, @CEP, @SENHA); ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@NOME", nome));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@DOC", doc));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@RG_IE", rg_ie));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@ENDERECO", endereco));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@NUMERO", numero));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@BAIRRO", bairro));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CIDADE", cidade));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@ESTADO", estado));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@CEP", cep));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@SENHA", senha));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection  = conexaoMySQL;
                    comandoMySQL.ExecuteNonQuery();

                    if(comandoMySQL.LastInsertedId != 0)
                    {
                        idcli = Convert.ToInt32(comandoMySQL.LastInsertedId);
                    }

                    //sql = new StringBuilder();
                    //sql.Append("SELECT LAST_INSERT_ID");

                    //comandoMySQL.CommandText = sql.ToString();

                    //dadosSelect.Load(comandoMySQL.ExecuteReader());

                    //idcli = Convert.ToInt32(dadosSelect.Rows[0][0].ToString());

                    return idcli;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
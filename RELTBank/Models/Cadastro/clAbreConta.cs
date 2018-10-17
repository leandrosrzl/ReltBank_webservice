using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models.Cadastro
{
    public class clAbreConta
    {
        MySqlConnection conexaoMySQL;
        MySqlCommand comandoMySQL;
        StringBuilder sqlInsert;
        clConta conta;
        Models.Transferencias.clInserirMovimentacao insertMov;
        int idcli_origem = 0;
        int agencia_origem = 6469;
        int conta_origem = 0;

        public int abrirConta(int idcli, int agencia, int tipo)
        {
            try
            {
                int idConta = 0;
                comandoMySQL = new MySqlCommand();
                sqlInsert    = new StringBuilder();
                insertMov    = new Transferencias.clInserirMovimentacao();

                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sqlInsert.Append("INSERT INTO CONTAS (AGENCIA, IDCLI, TIPO) VALUES (@AGENCIA, @IDCLI, @TIPO) ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA", agencia));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@IDCLI", idcli));
                    comandoMySQL.Parameters.Add(new MySqlParameter("@TIPO", tipo));

                    comandoMySQL.CommandText = sqlInsert.ToString();
                    comandoMySQL.Connection  = conexaoMySQL;
                    comandoMySQL.ExecuteNonQuery();

                    if (comandoMySQL.LastInsertedId != 0)
                    {
                        idConta = Convert.ToInt32(comandoMySQL.LastInsertedId);
                        conta = new clConta();
                        conta.agencia = agencia;
                        conta.conta = idConta;
                        ///criaMovInicial(conta, idcli);
                        insertMov.insereMovimentacao(0, idcli_origem, agencia_origem, conta_origem, idcli, agencia, idConta, "SALDO INICIAL", DateTime.Now);
                    }

                    return idConta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
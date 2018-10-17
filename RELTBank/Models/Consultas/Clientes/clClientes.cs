using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace RELTBank.Models.Consultas.Clientes
{
    public class clClientes
    {
        MySqlCommand comandoMySQL;
        MySqlConnection conexaoMySQL;
        DataTable dadosTabela;
        StringBuilder sql;
        clCliente Cliente;
        clConta Conta;
        clPessoa Pessoa;

        public clPessoa consultarCliente(int tipo, int idcli, int agencia, int conta, string senha) //tipo de consulta 0 = agencia/conta/senha 1 = idcli
        {
            comandoMySQL = new MySqlCommand();
            dadosTabela = new DataTable();
            sql = new StringBuilder();
            Cliente = new clCliente();
            Conta = new clConta();
            Pessoa = new clPessoa();

            try
            {
                using (conexaoMySQL = new MySqlConnection(clConexao.stringConexao))
                {
                    conexaoMySQL.Open();

                    sql.Append("SELECT " +
                        " CL.IDCLI, CL.NOME, CL.DOC, CL.RG_IE, CL.ENDERECO, CL.NUMERO, CL.BAIRRO, CL.CIDADE, CL.ESTADO, CL.CEP, " +
                        " CO.SALDO, CO.TIPO, CO.STATUS AS 'CO.STATUS', CL.STATUS AS 'CL.STATUS', CL.DATACAD AS 'CL.DATACAD', CO.AGENCIA, CO.CONTA " +
                        " FROM CLIENTES CL INNER JOIN CONTAS CO ON CL.IDCLI = CO.IDCLI ");
                    if (tipo == 0)
                    {
                        sql.Append("WHERE CO.AGENCIA = @AGENCIA AND CL.SENHA = @SENHA AND (CO.STATUS = 'A') ");

                        comandoMySQL.Parameters.Add(new MySqlParameter("@AGENCIA", agencia));
                        comandoMySQL.Parameters.Add(new MySqlParameter("@SENHA", senha));
                    }
                    else
                    {
                        sql.Append(" WHERE CL.IDCLI = @IDCLI ");

                        comandoMySQL.Parameters.Add(new MySqlParameter("@IDCLI", idcli));
                    }

                    sql.Append(" AND CO.CONTA = @CONTA AND(CO.STATUS = 'A') ");

                    comandoMySQL.Parameters.Add(new MySqlParameter("@CONTA", conta));

                    comandoMySQL.CommandText = sql.ToString();
                    comandoMySQL.Connection = conexaoMySQL;
                    dadosTabela.Load(comandoMySQL.ExecuteReader());

                    Cliente.idcli = Convert.ToInt32(dadosTabela.Rows[0]["IDCLI"].ToString());
                    Cliente.nome = dadosTabela.Rows[0]["NOME"].ToString();
                    Cliente.doc = dadosTabela.Rows[0]["DOC"].ToString();
                    Cliente.rg_ie = dadosTabela.Rows[0]["RG_IE"].ToString();
                    Cliente.endereco = dadosTabela.Rows[0]["ENDERECO"].ToString();
                    Cliente.numero = dadosTabela.Rows[0]["NUMERO"].ToString();
                    Cliente.bairro = dadosTabela.Rows[0]["BAIRRO"].ToString();
                    Cliente.cidade = dadosTabela.Rows[0]["CIDADE"].ToString();
                    Cliente.estado = dadosTabela.Rows[0]["ESTADO"].ToString();
                    Cliente.cep = dadosTabela.Rows[0]["CEP"].ToString();
                    Cliente.status = dadosTabela.Rows[0]["CL.STATUS"].ToString();
                    Cliente.datacad = Convert.ToDateTime((dadosTabela.Rows[0]["CL.DATACAD"].ToString()));

                    Conta.agencia = Convert.ToInt32(dadosTabela.Rows[0]["AGENCIA"].ToString());
                    Conta.conta = Convert.ToInt32(dadosTabela.Rows[0]["CONTA"].ToString()); ;
                    Conta.saldo = Convert.ToDouble(dadosTabela.Rows[0]["SALDO"].ToString());
                    Conta.tipo = Convert.ToInt32(dadosTabela.Rows[0]["TIPO"].ToString());
                    Conta.status = dadosTabela.Rows[0]["CO.STATUS"].ToString();

                    Pessoa.cliente = Cliente;
                    Pessoa.conta = Conta;

                    return Pessoa;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
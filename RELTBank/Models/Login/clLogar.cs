using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace RELTBank.Models
{
    public class clLogar
    {
        Models.Consultas.Clientes.clClientes consultaCliente;
        clPessoa Pessoa;

        public clPessoa logar(int agencia, int conta, string senha)
        {
            Pessoa          = new clPessoa();
            consultaCliente = new Consultas.Clientes.clClientes();

            Pessoa          = consultaCliente.consultarCliente(0, 0, agencia, conta, senha);

            return Pessoa;
        }
    }
}
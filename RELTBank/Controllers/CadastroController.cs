using RELTBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RELTBank.Controllers
{
    public class CadastroController : ApiController
    {
        int idcli = 0;
        int conta = 0;
        Models.Cadastro.clCadastro cadastro;
        Models.Retornos.clRetornoAbreConta retornoAbreConta;
        Models.Consultas.Clientes.clClientes consultaClientes = new Models.Consultas.Clientes.clClientes();
        Models.Cadastro.clAbreConta abreConta = new Models.Cadastro.clAbreConta();
        Models.Consultas.clValidaCpf validaCpf = new Models.Consultas.clValidaCpf();
        Models.Consultas.clValidaCNPJ validaCNPJ = new Models.Consultas.clValidaCNPJ();
        clPessoa Pessoa = new clPessoa();

        public bool validarDoc(string doc)
        {
            bool valido = false;
            //string mensagem = "CFP/CNPJ Inválido";
            if (validaCpf.isCPF(doc))
            {
                valido = true;
                //mensagem = "CPF Válido";
            }
            else if (validaCNPJ.isCnpj(doc))
            {
                valido = true;
                //mensagem = "CNPJ Válido";
            }

            /*retornoGeral = new Models.Retornos.clRetornoPost();
            retornoGeral.status = valido;
            retornoGeral.mensagem = mensagem;

            return retornoGeral;*/
            return valido;
        }

        private void pvCadastro(string nome, string doc, string rg_ie, string endereco, string numero, string bairro, string cidade,
                              string estado, string cep, int tipo, string senha)
        {
            try
            {
                retornoAbreConta = new Models.Retornos.clRetornoAbreConta();
                cadastro = new Models.Cadastro.clCadastro();
                if (validarDoc(doc))
                {
                    idcli = cadastro.cadastrar(nome, doc, rg_ie, endereco, numero, bairro, cidade, estado, cep, senha);

                    conta = abreConta.abrirConta(idcli, 6469, tipo);

                    Pessoa = consultaClientes.consultarCliente(1, idcli, 0, conta, "");

                    retornoAbreConta.status = 1;
                    retornoAbreConta.mensagem = "Cliente cadastrado com sucesso";
                    retornoAbreConta.conta = Pessoa.conta;
                }
                else
                {
                    int a, b = 0;
                    a = 2 / b;
                }
            }
            catch(DivideByZeroException) //Exception causado intencionalmente para caso o documento seja inválido
            {
                retornoAbreConta.status = 2;
                retornoAbreConta.mensagem = "Documento CPF/CNPJ inválido";
            }
            catch (Exception ex)
            {
                retornoAbreConta.status   = 0;
                retornoAbreConta.mensagem = ex.ToString();
            }
        }

        /*// GET: api/Cadastro
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/Cadastro/5
        

        // POST: api/Cadastro
        public Models.Retornos.clRetornoAbreConta Post(string nome, string doc, string rg_ie, string endereco, string numero, string bairro, string cidade,
                              string estado, string cep, int tipo, string senha)
        {
            pvCadastro(nome, doc, rg_ie, endereco, numero, bairro, cidade, estado, cep, tipo, senha);
            return retornoAbreConta;
        }

        /*// PUT: api/Cadastro/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cadastro/5
        public void Delete(int id)
        {
        }*/
    }
}

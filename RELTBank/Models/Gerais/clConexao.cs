using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RELTBank.Models
{
    public class clConexao
    {
        private static string mySqlConn = @"Server=mysql785.umbler.com;Port=41890;Database=dbreltbank; Uid=usreltbank; Pwd=JacareNavioFaca;";

        public static string stringConexao
        {
            get { return mySqlConn; }
        }
    }
}
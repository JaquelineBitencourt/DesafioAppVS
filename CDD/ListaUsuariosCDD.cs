using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MDL;

namespace CDD
{
    public class ListaUsuariosCDD
    {
        //criar conexão com o banco 
        private SqlConnection CriarConexao() => new SqlConnection();

    }

    internal class SqlConnection
    {
    }

    //parametros que vão para o banco
    //using (SqlCommand sqlCommand = new SqlCommand(CriarConexao));



}

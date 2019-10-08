using System;
using System.Collections.Generic;
using CEF.Modelos;
using CEF;
using System.Linq;


namespace CDD
{
    public class UsuarioCDD
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
        */
        public static UsuarioCDD instancia;
        public UsuarioCDD() { }
        public static UsuarioCDD Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioCDD();
                return instancia;
            }
        }
        //public Usuario ValidaUsuario(Usuario Usuario)
        //{
        //    Usuario.NomeDoUsuario = "Luis " + Usuario.NomeDoUsuario;
        //    return Usuario;
        //}


        public IEnumerable<Usuarios> BuscaUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                lista = (from a in db.Usuarios
                         where a.IdUsuario >= 1
                         select new Usuarios
                         {
                             IdUsuario = 0,
                             NomeDoUsuario = a.NomeDoUsuario,
                             Logado = a.Logado,
                             Chimarreando = a.Chimarreando

                         }).ToList();
            }

            return lista;
        }

    }
}

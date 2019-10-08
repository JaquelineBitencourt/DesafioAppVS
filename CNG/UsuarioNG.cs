using System;
using MDL;
using CDD;
using CEF;
using System.Collections.Generic;

namespace CNG
{
    public class UsuarioNG
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
         */
        public static UsuarioNG instancia;
        public UsuarioNG() { }
        public static UsuarioNG Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioNG();
                return instancia;
            }
        }
        //public Usuario ValidaUsuario(Usuario Pessoa)
        //{
        //    //return UsuarioCDD.Instancia.ValidaUsuario(Pessoa);
        //}

        public IEnumerable<CEF.Modelos.Usuarios> BuscaUsuarios()
        {
            UsuarioCDD usuarioCDD = new UsuarioCDD();

            return UsuarioCDD.Instancia.BuscaUsuarios();


        }
    }
}

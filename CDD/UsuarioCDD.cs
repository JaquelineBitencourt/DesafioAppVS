using System;
using MDL;

namespace CDD
{
    public class UsuarioCDD
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
        */
        private static UsuarioCDD instancia;
        private UsuarioCDD() { }
        public static UsuarioCDD Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioCDD();
                return instancia;
            }
        }
        public Usuario ValidaUsuario(Usuario Usuario)
        {
            Usuario.NomeDoUsuario = "Luis " + Usuario.NomeDoUsuario;
            return Usuario;
        }
    }
}

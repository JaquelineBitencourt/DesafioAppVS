using System;
using MDL;
using CDD;
namespace CNG
{
    public class UsuarioNG
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
         */
        private static UsuarioNG instancia;
        private UsuarioNG() { }
        public static UsuarioNG Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioNG();
                return instancia;
            }
        }
        public bool ValidaUsuario(Usuario Pessoa)
        {
            return UsuarioCDD.Instancia.ValidaUsuario(Pessoa);
        }
    }
}
